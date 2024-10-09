using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    Enemy enemyComponent;

    protected override void Start()
    {
        base.Start();

        enemyComponent = GetComponent<Enemy>();
    }

    public override void ReduceHp(Entity subject) {
        base.ReduceHp(subject);
    }

    protected override void Die() {
        base.Die();

        enemyComponent.Die();
    }
}
