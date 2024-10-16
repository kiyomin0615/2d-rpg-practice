using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    public GameObject thrownSword { get; private set; }

    public bool isDoingSomething { get; private set; }

    [Header("Move")]
    public float moveSpeed = 8f;
    public float jumpForce = 16f;
    public float knockbackSpeed = 3f;

    [Header("Dash")]
    public float dashSpeed = 25f;
    public float dashDuration = 0.2f;
    public float dashDir { get; private set; }

    [Header("Battle")]
    public Vector2[] attackVelocityList;

    [Header("Drop")]
    [SerializeField] GameObject dropPrefab;

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
    public PlayerDieState dieState { get; private set; }
    public PlayerAimState aimState { get; private set; }
    public PlayerCatchState catchState { get; private set; }
    public PlayerUltimateState ultimateState { get; private set; }
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
        dieState = new PlayerDieState(this, stateMachine, "Die");
        aimState = new PlayerAimState(this, stateMachine, "Aim");
        catchState = new PlayerCatchState(this, stateMachine, "Catch");
        ultimateState = new PlayerUltimateState(this, stateMachine, "Jump");
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

    public void AssignNewSword(GameObject sword)
    {
        thrownSword = sword;
    }

    public void CatchSword()
    {
        stateMachine.ChangeState(catchState);
        Destroy(thrownSword);
    }

    public bool HasSword()
    {
        if (!thrownSword)
        {
            return true;
        }

        thrownSword.GetComponent<PlayerSword>().GoBackToPlayer();
        return false;
    }

    // Coroutine
    public IEnumerator WaitForDoingSomething(float _seconds)
    {
        isDoingSomething = true;
        yield return new WaitForSeconds(_seconds); // wait for seconds
        isDoingSomething = false;
    }

    public override void Die()
    {
        base.Die();

        DropAllEquipments();

        stateMachine.ChangeState(dieState);
    }

    public void OnExitAnimation()
    {
        stateMachine.currentState.OnExitAnimation();
    }

    public void ExitUltimateState()
    {
        stateMachine.ChangeState(airState);
    }

    private void CheckDashInput()
    {
        if (IsWallDetected())
            return;

        // State Machine 패턴에 위배된다
        // 어떤 상태에서든 대쉬 상태로 전이가 가능하다
        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dashSkill.TrySkill())
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;
            stateMachine.ChangeState(dashState);
        }
    }

    void DropItem(ItemData itemData)
    {
        GameObject dropItemObject = Instantiate(dropPrefab, transform.position, Quaternion.identity);
        dropItemObject.GetComponent<ItemObject>().SetupItemData(itemData);
    }

    void DropAllEquipments()
    {
        List<Item> dropEquipmentObjectList = new List<Item>();

        for (int i = 0; i < Inventory.instance.equipments.Count; i++)
        {
            dropEquipmentObjectList.Add(Inventory.instance.equipments[i]);
        }

        for (int i = 0; i < dropEquipmentObjectList.Count; i++)
        {
            Item dropEquipment = dropEquipmentObjectList[i];
            DropItem(dropEquipment.itemData);
            Inventory.instance.UnEquip(dropEquipment.itemData as EquipmentData);
        }
    }
}
