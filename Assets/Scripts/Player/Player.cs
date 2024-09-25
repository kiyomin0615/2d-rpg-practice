using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isDoingSomething { get; private set; }

    [Header("Move")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;

    [Header("Dash")]
    [SerializeField] private float dashCoolDown = 5f;
    [SerializeField] private float dashTimer = 0f;
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    public float dashDir { get; private set; }

    [Header("Attack")]
    public Vector2[] attackVelocityList;

    [Header("Collision")]
    [SerializeField] Transform groundChecker;
    [SerializeField] float distToGround;
    [SerializeField] Transform wallChecker;
    [SerializeField] float distToWall;
    [SerializeField] LayerMask groundLayer;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region State
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Init(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        CheckDashInput();
    }

    // Coroutine
    public IEnumerator WaitForDoingSomething(float _seconds)
    {
        isDoingSomething = true;
        yield return new WaitForSeconds(_seconds); // wait for seconds
        isDoingSomething = false;
    }

    private void CheckDashInput()
    {
        if (IsWallDetected())
            return;

        dashTimer -= Time.deltaTime;
        // State Machine 패턴에 위배된다
        // 어떤 상태에서든 대쉬 상태로 전이가 가능하다
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0)
        {   
            dashTimer = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;
            stateMachine.ChangeState(dashState);
        }
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        ControlFlip(_xVelocity);
    }

    public bool IsGroundDetected()
    {
       return Physics2D.Raycast(groundChecker.position, Vector2.down, distToGround, groundLayer);
    }

    public bool IsWallDetected()
    {
       return Physics2D.Raycast(wallChecker.position, Vector2.right * facingDir, distToWall, groundLayer);
    }

    public void ControlFlip(float _xInput)
    {
        if ((_xInput > 0 && !facingRight) || (_xInput < 0 && facingRight))
            Flip();
    }

    public void Flip()
    {
        facingDir = -facingDir;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(groundChecker.position, new Vector2(groundChecker.position.x, groundChecker.position.y - distToGround));
        Gizmos.DrawLine(wallChecker.position, new Vector2(wallChecker.position.x + distToWall, wallChecker.position.y));
    }

    public void TriggerAnimationEvent()
    {
        stateMachine.currentState.TriggerAnimationEventOnState();
    }
    
}
