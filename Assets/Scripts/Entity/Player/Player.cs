using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public bool isDoingSomething { get; private set; }

    [Header("Move")]
    public float moveSpeed = 8f;
    public float jumpForce = 16f;

    [Header("Dash")]
    [SerializeField] private float dashCoolDown = 2f;
    [SerializeField] private float dashTimer = 0f;
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    public float dashDir { get; private set; }

    [Header("Battle")]
    public Vector2[] attackVelocityList;
    public float counterAttackDuration;

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
    public PlayerCounterAttackState counterAttackState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Init(idleState);
    }

    protected override void Update()
    {
        base.Update();

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

    public void OnExitAnimation()
    {
        stateMachine.currentState.OnExitAnimation();
    }
}
