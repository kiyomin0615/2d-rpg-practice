using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Basic Stats")]
    public Stat strength; // 1 strength = 1 damage + 1% critical damage
    public Stat agility; // 1 agility = 1 evasion + 1% critical chance
    public Stat vitality; // 1 vitality = 5 HP

    [Header("Other Stats")]
    public Stat damage;
    public Stat criticalChange; // percentage
    public Stat criticalDamagePercentage; // percentage
    public Stat basicHP;
    public Stat armor;
    public Stat evasion;

    [SerializeField] public int currentHP;

    public Action onHPChanged = null;

    public bool isDead { get; private set; } = false;

    protected virtual void Start()
    {
        currentHP = CalculateMaxHP();
        criticalDamagePercentage.SetDefaultValue(150); // 150%
    }

    public virtual void ReduceHP(Entity subject)
    {
        if (CanAvoidAttack())
            return;

        int totalDamage = subject.stats.damage.GetValue() + subject.stats.strength.GetValue();
        if (IsCriticalHit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CalculateTotalDamage(totalDamage);

        currentHP -= totalDamage;

        onHPChanged(); // Update HP Bar UI

        Debug.Log($"Damage: {totalDamage}");

        if (currentHP < 0 && !isDead)
        {
            Die();
        }
    }

    public int CalculateMaxHP()
    {
        return basicHP.GetValue() + vitality.GetValue() * 5;
    }

    protected virtual void Die()
    {
        isDead = true;
    }

    int CalculateTotalDamage(int damage)
    {
        int totalDamage = damage - armor.GetValue();
        return Mathf.Clamp(totalDamage, 0, int.MaxValue);
    }

    int CalculateCriticalDamage(int damage)
    {
        float totalCriticalMultiplier = (criticalDamagePercentage.GetValue() + strength.GetValue()) * 0.01f;
        float criticalDamage = damage * totalCriticalMultiplier;
        return Mathf.RoundToInt(criticalDamage);
    }

    bool IsCriticalHit()
    {
        int totalCriticalChange = criticalChange.GetValue() + agility.GetValue();
        if (UnityEngine.Random.Range(0, 100) < totalCriticalChange)
        {
            return true;
        }

        return false;
    }

    bool CanAvoidAttack()
    {
        int totalEvasion = evasion.GetValue() + agility.GetValue();
        if (UnityEngine.Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }

        return false;
    }

}
