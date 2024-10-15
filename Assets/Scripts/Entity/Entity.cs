using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityEffects effects { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public CapsuleCollider2D capsuleCollider { get; private set; }
    public Stats stats { get; private set; }
    #endregion

    [Header("Collision")]
    [SerializeField] protected Transform groundChecker;
    [SerializeField] protected float distToGround;
    [SerializeField] protected Transform wallChecker;
    [SerializeField] protected float distToWall;
    [SerializeField] protected LayerMask groundLayer;
    public Transform attackChecker;
    public float attackCheckerRadius;

    public int facingDir { get; private set; } = 1;
    public bool facingRight = true;

    public Action onFlip = null;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        effects = GetComponent<EntityEffects>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        stats = GetComponent<Stats>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    public virtual bool IsGroundDetected()
    {
        return Physics2D.Raycast(groundChecker.position, Vector2.down, distToGround, groundLayer);
    }

    public virtual bool IsWallDetected()
    {
        return Physics2D.Raycast(wallChecker.position, Vector2.right * facingDir, distToWall, groundLayer);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundChecker.position, new Vector2(groundChecker.position.x, groundChecker.position.y - distToGround));
        Gizmos.DrawLine(wallChecker.position, new Vector2(wallChecker.position.x + (distToWall * facingDir), wallChecker.position.y));
        Gizmos.DrawWireSphere(attackChecker.position, attackCheckerRadius);
    }

    public virtual void ControlFlip(float _xInput)
    {
        if ((_xInput > 0 && !facingRight) || (_xInput < 0 && facingRight))
            Flip();
    }

    public virtual void Flip()
    {
        facingDir = -facingDir;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if (onFlip != null)
        {
            onFlip();
        }
    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        ControlFlip(_xVelocity);
    }

    public virtual void TakeDamage(Entity subject)
    {
        stats.ReduceHP(subject);
        effects.StartCoroutine("ApplyHitEffect");
    }

    public void Appear()
    {
        spriteRenderer.color = Color.white;
    }

    public void Disappear()
    {
        spriteRenderer.color = Color.clear;
    }

    public virtual void Die()
    {
    }
}
