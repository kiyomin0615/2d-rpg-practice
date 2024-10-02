using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    Skeleton skeleton;

    public SkeletonMoveState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animatorParam, Skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animatorParam)
    {
        this.skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        skeleton.SetVelocity(skeleton.moveSpeed * skeleton.facingDir, skeleton.rb.velocity.y);

        if (skeleton.IsWallDetected() || !skeleton.IsGroundDetected()) {
            skeleton.Flip();
            enemyStateMachine.ChangeState(skeleton.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
