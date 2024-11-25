using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    Skeleton skeleton;
    Transform playerTransform;

    int moveDir;
    float lastAttackTime;

    public SkeletonBattleState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animatorParam, Skeleton _skeleton) : base(_enemy, _enemyStateMachine, _animatorParam)
    {
        this.skeleton = _skeleton;
    }

    public override void Enter()
    {
        base.Enter();

        playerTransform = PlayerManager.instance.player.transform;

        stateTimer = skeleton.battleDuration;
    }

    public override void Update()
    {
        base.Update();

        if (playerTransform.position.x > skeleton.transform.position.x)
        {
            moveDir = 1;
        }
        else if (playerTransform.position.x < skeleton.transform.position.x)
        {
            moveDir = -1;
        }

        skeleton.SetVelocity(skeleton.moveSpeed * moveDir, skeleton.rb.velocity.y);

        if (skeleton.isPlayerDetected())
        {
            if (skeleton.isPlayerDetected().distance < skeleton.attackRange && Time.time > lastAttackTime + skeleton.attackCooldown)
            {
                enemyStateMachine.ChangeState(skeleton.attackState);
                lastAttackTime = Time.time;
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(skeleton.transform.position, playerTransform.position) > skeleton.battleRange)
            {
                enemyStateMachine.ChangeState(skeleton.idleState);
            }
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
