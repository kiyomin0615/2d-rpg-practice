using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationEventHandler : MonoBehaviour
{
    private Enemy enemyComponent;

    public void Awake()
    {
        enemyComponent = GetComponentInParent<Enemy>();
    }

    private void OnExitAnimation()
    {
        enemyComponent.OnExitAnimation();
    }

    private void OnHit() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(enemyComponent.attackChecker.position, enemyComponent.attackCheckerRadius);

        foreach(Collider2D hitCollider in hitColliders) {
            if (hitCollider.GetComponent<Player>() != null) {
                hitCollider.GetComponent<Player>().TakeDamage();
            }
        }
    }
}
