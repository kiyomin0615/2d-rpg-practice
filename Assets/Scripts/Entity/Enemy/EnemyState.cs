using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected Rigidbody2D enemyRb;
    protected EnemyStateMachine enemyStateMachine;

    protected float stateTimer;
    protected float xInput;
    protected float yInput;
    private string animatorParam;

    protected bool animationFinished;

    public EnemyState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animatorParam)
    {
        this.enemy = _enemy;
        this.enemyStateMachine = _enemyStateMachine;
        this.animatorParam = _animatorParam;
    }

    public virtual void Enter()
    {
        enemy.animator.SetBool(animatorParam, true);
        enemyRb = enemy.rb;
        animationFinished = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        enemy.animator.SetBool(animatorParam, false);
        enemy.SetLastAnimatorParam(animatorParam);
    }

    public virtual void OnExitAnimation()
    {
        animationFinished = true;
    }
}
