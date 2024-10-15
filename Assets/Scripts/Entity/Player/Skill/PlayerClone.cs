using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerClone : MonoBehaviour
{
    Animator animator;
    // SpriteRenderer spriteRenderer;

    public Transform attackChecker;
    public float attackCheckerRadius = 1f;

    public bool canAttack = false;

    float timer;

    void Awake()
    {
        animator = GetComponent<Animator>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        timer = SkillManager.instance.cloneSkill.cloneDuration;
        canAttack = true;
        Attack();
        FaceEneny();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            // spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a);
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        if (canAttack)
        {
            animator.SetInteger("Attack", Random.Range(1, 3));
        }
    }

    void FaceEneny()
    {
        Transform targetTransform = null;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25f);

        float closestDist = 999;

        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                float dist = Vector2.Distance(transform.position, collider.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    targetTransform = collider.transform;
                }
            }
        }

        if ((targetTransform != null) && (transform.position.x > targetTransform.position.x))
        {
            transform.Rotate(0, 180f, 0);
        }
    }

    private void OnExitAnimation()
    {
        timer = -0.1f; // less than 0
    }

    private void OnHit()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackChecker.position, attackCheckerRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            Enemy enemyComponent = hitCollider.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(PlayerManager.instance.player);
            }
        }
    }
}
