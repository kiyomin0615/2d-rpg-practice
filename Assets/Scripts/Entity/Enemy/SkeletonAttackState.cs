using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    Skeleton skeleton;

    public SkeletonAttackState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animatorParam, Skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animatorParam)
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

        skeleton.SetVelocity(0f, 0f);

        if (animationEventTriggered) {
            enemyStateMachine.ChangeState(skeleton.battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
