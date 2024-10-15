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

    public override void ReduceHP(Entity subject)
    {
        base.ReduceHP(subject);
    }

    protected override void Die()
    {
        base.Die();

        enemyComponent.Die();
    }
}
