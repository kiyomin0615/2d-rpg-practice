using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventHandler : MonoBehaviour
{
    private Player playerComponent;

    public void Awake()
    {
        playerComponent = GetComponentInParent<Player>();
    }

    private void OnExitAnimation()
    {
        AudioManager.instance.PlaySFX(2);
        playerComponent.OnExitAnimation();
    }

    private void OnHit()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(playerComponent.attackChecker.position, playerComponent.attackCheckerRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            Enemy enemyComponent = hitCollider.GetComponent<Enemy>();
            EnemyStats enemyStats = hitCollider.GetComponent<EnemyStats>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(playerComponent);
            }
        }
    }

    private void OnHitCounter()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(playerComponent.attackChecker.position, playerComponent.attackCheckerRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            Enemy enemyComponent = hitCollider.GetComponent<Enemy>();
            EnemyStats enemyStats = hitCollider.GetComponent<EnemyStats>();
            if (enemyComponent != null)
            {
                enemyComponent.TakeDamage(playerComponent);
                enemyComponent.Stun();
            }
        }
    }

    private void OnThrow()
    {
        SkillManager.instance.swordSkill.CreateSword();
    }
}
