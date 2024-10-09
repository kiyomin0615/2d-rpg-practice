using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public Stat maxHp;
    public Stat damage;
    public Stat strength;

    [SerializeField] public int currentHp;

    protected virtual void Start()
    {
        currentHp = maxHp.GetValue();
    }

    public virtual void ReduceHp(Entity subject) {
        int totalDamage = subject.stats.damage.GetValue() + subject.stats.strength.GetValue();
        currentHp -= totalDamage;
        Debug.Log($"Damage: {totalDamage}");

        if (currentHp < 0) {
            Die();
        }
    }

    protected virtual void Die() {
        //
    }
}
