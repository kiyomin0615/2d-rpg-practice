using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    Enemy enemyComponent;

    [Header("Level Details")]
    [SerializeField] int level = 1;

    [Range(0f, 1f)]
    [SerializeField] float multiplier = 0.4f;


    protected override void Start()
    {
        ApplyAllModifiers();

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

    void ModifyStatByLevel(Stat stat)
    {
        for (int i = 1; i < level; i++)
        {
            stat.AddModifier((int)(stat.GetValue() * multiplier));
        }
    }

    void ApplyAllModifiers()
    {
        ModifyStatByLevel(strength);
        ModifyStatByLevel(agility);
        ModifyStatByLevel(vitality);
        ModifyStatByLevel(damage);
        ModifyStatByLevel(criticalChange);
        ModifyStatByLevel(criticalDamagePercentage);
        ModifyStatByLevel(basicHP);
        ModifyStatByLevel(armor);
        ModifyStatByLevel(evasion);
    }
}
