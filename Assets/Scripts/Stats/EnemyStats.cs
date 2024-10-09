using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    public override void ReduceHp(Entity subject) {
        base.ReduceHp(subject);
    }

    protected override void Die() {

    }
}
