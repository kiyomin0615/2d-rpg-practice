using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SkeletonStunState : EnemyState
{
    Skeleton skeleton;

    public SkeletonStunState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animatorParam, Skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animatorParam)
    {
        this.skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = skeleton.stunDuration;
        skeleton.SetVelocity(0f, 0f);
        skeleton.effects.EnterStunEffect();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            enemyStateMachine.ChangeState(skeleton.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        skeleton.effects.ExitStunEffect();
    }
}
