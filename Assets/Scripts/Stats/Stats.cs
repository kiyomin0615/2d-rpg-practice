using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public Stat maxHp;
    public Stat damage;

    [SerializeField] public int currentHp;

    void Start()
    {
        currentHp = maxHp.GetValue();

        // TMP
        damage.AddModifier(5);
    }

    public void ReduceHp(int damage) {
        currentHp -= damage;
        Debug.Log($"Damage: {damage}");

        if (currentHp < 0) {
            Die();
        }
    }

    void Die() {
        //
    }
}
