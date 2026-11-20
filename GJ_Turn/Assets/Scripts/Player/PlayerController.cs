using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public int fadeNum = 0;
    public bool isFlashOn = false;
    private Rigidbody2D rb;
    [SerializeField] private Light2D flash;
    [SerializeField] private Light2D spotlight;
    private Camera cam;
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
    }

    private void Start()
    {
        stateMachine.Init(idleState);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlashOn = !isFlashOn;
        }
        flash.enabled = isFlashOn;
        spotlight.pointLightInnerAngle = isFlashOn ? 330f : 360f;
        Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mouse - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        flash.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        spotlight.transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);
        stateMachine.curState.Update();
    }

    public void SetVelocity(Vector2 _velocity)
    {
        rb.velocity = _velocity;
    }
}