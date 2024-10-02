using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : EnemyState
{
    Skeleton skeleton;

    public SkeletonIdleState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animatorParam, Skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animatorParam)
    {
        this.skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = skeleton.idleDuration;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0) {
            enemyStateMachine.ChangeState(skeleton.moveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
