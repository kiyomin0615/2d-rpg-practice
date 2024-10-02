using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected Rigidbody2D enemyRb;
    protected EnemyStateMachine enemyStateMachine;
    protected bool animationEventTriggered;

    protected float stateTimer;
    protected float xInput;
    protected float yInput;
    private string animatorParam;

    public virtual void Enter()
    {
        enemy.animator.SetBool(animatorParam, true);
        enemyRb = enemy.rb;
        animationEventTriggered = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        enemy.animator.SetFloat("yVelocity", enemyRb.velocity.y);
    }

    public virtual void Exit()
    {
        enemy.animator.SetBool(animatorParam, false);
    }

    public virtual void TriggerAnimationEventOnState()
    {
        animationEventTriggered = true;
    }
}
