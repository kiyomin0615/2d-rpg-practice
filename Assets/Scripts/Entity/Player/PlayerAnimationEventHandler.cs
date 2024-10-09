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
        playerComponent.OnExitAnimation();
    }

    private void OnHit() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(playerComponent.attackChecker.position, playerComponent.attackCheckerRadius);

        foreach(Collider2D hitCollider in hitColliders) {
            Enemy enemyComponent = hitCollider.GetComponent<Enemy>();
            Stats enemyStats =hitCollider.GetComponent<Stats>();
            if (enemyComponent != null) {
                enemyComponent.TakeDamage();
                enemyStats.ReduceHp(playerComponent.stats.damage.GetValue());
            }
        }
    }

    private void OnHitCounter() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(playerComponent.attackChecker.position, playerComponent.attackCheckerRadius);

        foreach(Collider2D hitCollider in hitColliders) {
            Enemy enemyComponent = hitCollider.GetComponent<Enemy>();
            Stats enemyStats =hitCollider.GetComponent<Stats>();
            if (enemyComponent != null) {
                enemyComponent.TakeDamage();
                enemyStats.ReduceHp(playerComponent.stats.damage.GetValue());
                enemyComponent.Stun();
            }
        }
    }

    private void OnThrow() {
        SkillManager.instance.swordSkill.CreateSword();
    }
}
