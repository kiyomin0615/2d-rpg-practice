using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    protected Skeleton skeleton;

    public SkeletonGroundState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animatorParam, Skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animatorParam)
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

        if (skeleton.isPlayerDetected())
        {
            enemyStateMachine.ChangeState(skeleton.battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
