using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Skeleton : Enemy
{
    #region State
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunState stunState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        stunState = new SkeletonStunState(this, stateMachine, "Stun", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Init(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage() {
        base.TakeDamage();

        if (canBeStunned) {
            animator.SetBool("Stun", true);
            stateMachine.ChangeState(stunState);
        }
    }
}
