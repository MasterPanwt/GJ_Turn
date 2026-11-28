using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public bool isFlashOn = false;
    private int batteryCnt = 100;  // Only for testing
    private float dazzleTimer = 0f;
    public UnityEvent onDazzle;
    public Vector2 moveInput { get; private set; }
    private Rigidbody2D rb;
    private float flashAngle = 180f;
    private float currentFlashAngle = 180f;
    private float flashAngleVelocity;
    [SerializeField] private Light2D flash;
    [SerializeField] private Light2D spotlight;
    private readonly List<GameObject> fadeTargets = new List<GameObject>();
    private LayerMask obstacleMask;
    private int interactableMask;
    public Animator anim { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        obstacleMask = LayerMask.GetMask("Flash Obstacle");
        interactableMask = LayerMask.NameToLayer("Interactable");
        anim = GetComponent<Animator>();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
    }

    private void Start()
    {
        stateMachine.Init(idleState);
        foreach (GameObject target in fadeTargets)
        {
            Collider2D col = target.GetComponent<Collider2D>();
            if (!col)
                Debug.LogWarning($"{name} 没有碰撞箱", target);
            else
                col.enabled = false;
        }
    }

    private void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        anim.SetInteger("Horizontal", (int)Input.GetAxisRaw("Horizontal"));
        anim.SetInteger("Vertical", (int)Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlashOn = !isFlashOn;
        }
        flash.enabled = isFlashOn;
        spotlight.pointLightInnerAngle = isFlashOn ? 330f : 360f;
        if (moveInput != Vector2.zero)
        {
            flashAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg - 90f;
        }
        currentFlashAngle = Mathf.SmoothDampAngle(currentFlashAngle, flashAngle, ref flashAngleVelocity, 0.08f);
        flash.transform.rotation = Quaternion.Euler(0f, 0f, currentFlashAngle);
        spotlight.transform.rotation = Quaternion.Euler(0f, 0f, currentFlashAngle + 180f);
        dazzleTimer = Mathf.Clamp(dazzleTimer - Time.deltaTime, 0f, 0.4f);
        if (isFlashOn && batteryCnt > 0 && dazzleTimer == 0f && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Dazzle());
            batteryCnt--;
            dazzleTimer = 0.4f;
        }

        List<GameObject> embodieds = new List<GameObject>();
        foreach (GameObject target in fadeTargets)
        {
            FadeController fc = target.GetComponent<FadeController>();
            if (IsInFlash(target.transform.position) && isFlashOn)
            {
                fc.Rampto(1f);
            }
            else if (fc.progress != 1f) fc.Rampto(0f);//实现松手回退进度
            if (fc.progress == 1f)
            {
                Collider2D col = fc.GetComponent<Collider2D>();
                if (col != null)
                    col.enabled = true;
                target.layer = interactableMask;
                embodieds.Add(target);
            }
        }
        foreach (GameObject e in embodieds)
        {
            DelTarget(e);
        }
        stateMachine.curState.Update();
    }

    public void SetVelocity(Vector2 _velocity)
    {
        rb.velocity = _velocity;
    }

    public void AddTarget(GameObject gameObject)
    {
        if (!fadeTargets.Contains(gameObject))
        {
            fadeTargets.Add(gameObject);
        }
    }

    public void DelTarget(GameObject gameObject)
    {
        fadeTargets.Remove(gameObject);
    }

    private bool IsInFlash(Vector2 targetPos)
    {
        Vector2 origin = transform.position;
        Vector2 toTarget = targetPos - origin;
        float dist = toTarget.magnitude;
        if (dist > 8f)
        {
            return false;
        }
        float targetAngle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg - 90f;
        if (Mathf.Abs(Mathf.DeltaAngle(currentFlashAngle, targetAngle)) > 27.5f)
        {
            return false;
        }
        RaycastHit2D hit = Physics2D.Raycast(origin, toTarget.normalized, dist, obstacleMask);
        return hit.collider == null;
    }

    public float GetCurrentFlashAngle()
    {
        return currentFlashAngle;
    }

    IEnumerator Dazzle()
    {
        onDazzle?.Invoke();
        float timer = 0f;
        while (timer <= 0.02f)
        {
            flash.volumeIntensity = 3.5f * (1f - (0.02f - timer) / 0.02f * (0.02f - timer) / 0.02f * (0.02f - timer) / 0.02f);
            timer += Time.deltaTime;
            yield return null;
        }
        while (timer <= 0.4f)
        {
            flash.volumeIntensity = 3.5f * (1f - (timer - 0.02f) / 0.38f * (timer - 0.02f) / 0.38f);
            timer += Time.deltaTime;
            yield return null;
        }
        flash.volumeIntensity = 0f;
    }
}