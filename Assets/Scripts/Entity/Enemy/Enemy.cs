using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{    
    [SerializeField] protected LayerMask playerLayer;

    [Header("Move")]
    public float moveSpeed = 3f;
    public float idleDuration = 2f;


    [Header("Battle")]
    public float battleRange = 10f;
    public float battleDuration = 5f;
    public float attackRange = 2.25f;
    public float attackCooldown = 1f;
    public float stunDuration = 1f;
    public bool stunnable = false;

    #region State
    public EnemyStateMachine stateMachine { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + (attackRange * facingDir), transform.position.y));
    }

    public virtual RaycastHit2D isPlayerDetected() {
        return Physics2D.Raycast(wallChecker.position, Vector2.right * facingDir, battleRange, playerLayer);
    }
    
    public void OnExitAnimation()
    {
        stateMachine.currentState.OnExitAnimation();
    }

    public virtual void EnableCounterAttack() {
        stunnable = true;
    }

    public virtual void DisableCounterAttack() {
        stunnable = false;
    }

    public virtual void Stun() {}

    public virtual void Freeze() {
        moveSpeed = 0f;
        animator.SetBool("Move", false);
    }

    public virtual void Unfreeze() {
        moveSpeed = 3f;
        animator.SetBool("Move", true);
    }
}
