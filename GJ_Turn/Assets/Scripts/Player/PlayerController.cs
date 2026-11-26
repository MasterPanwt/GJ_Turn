using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public int fadeNum = 0;
    public bool isFlashOn = false;
    public Vector2 moveInput { get; private set; }
    private Camera cam;
    private Rigidbody2D rb;
    private float flashAngle = 180f;
    [SerializeField] private Light2D flash;
    [SerializeField] private Light2D spotlight;
    private readonly List<GameObject> fadeTargets = new List<GameObject>();
    [SerializeField]  private LayerMask obstacleMask;
    [SerializeField]  private int interactableMask;
    public Animator anim { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    private void Awake()
    {
        cam = Camera.main;
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
        flash.transform.rotation = Quaternion.Euler(0f, 0f, flashAngle);
        spotlight.transform.rotation = Quaternion.Euler(0f, 0f, flashAngle + 180f);

        List<GameObject> embodieds = new List<GameObject>();
        foreach (GameObject target in fadeTargets)
        {
            FadeController fc = target.GetComponent<FadeController>();
            if (IsInFlash(target.transform.position) && isFlashOn)
            {
                fc.Rampto(1f);
            }
            else if (fc.progress != 1f) fc.Rampto(0f);//实现松手回退进度
            else
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
        if (Mathf.Abs(Mathf.DeltaAngle(flashAngle, targetAngle)) > 27.5f)
        {
            return false;
        }
        RaycastHit2D hit = Physics2D.Raycast(origin, toTarget.normalized, dist, obstacleMask);
        return hit.collider == null;
    }
}