using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Strength,
    Agility,
    Vitality,
    Damage,
    CriticalChance,
    CriticalDamage,
    BasicHP,
    Armor,
    Evasion
}

public class Stats : MonoBehaviour
{
    [Header("Basic Stats")]
    public Stat strength; // 1 strength = 1 damage + 1% critical damage
    public Stat agility; // 1 agility = 1 evasion + 1% critical chance
    public Stat vitality; // 1 vitality = 5 HP

    [Header("Other Stats")]
    public Stat damage;
    public Stat criticalChancePercentage; // percentage
    public Stat criticalDamagePercentage; // percentage
    public Stat basicHP;
    public Stat armor;
    public Stat evasion;

    [SerializeField] public int currentHP;

    public Action onHPChanged = null;

    public bool isDead { get; private set; } = false;

    protected virtual void Awake()
    {
        criticalChancePercentage.SetDefaultValue(10); // 10%
        criticalDamagePercentage.SetDefaultValue(150); // 150%
    }

    protected virtual void Start()
    {
        currentHP = CalculateMaxHP();
    }

    public int GetStatValue(StatType statType)
    {
        int value = 0;

        switch (statType)
        {
            case StatType.Strength:
                value = strength.GetValue();
                break;
            case StatType.Agility:
                value = agility.GetValue();
                break;
            case StatType.Vitality:
                value = vitality.GetValue();
                break;
            case StatType.Damage:
                value = damage.GetValue();
                break;
            case StatType.CriticalChance:
                value = criticalChancePercentage.GetValue();
                break;
            case StatType.CriticalDamage:
                value = criticalDamagePercentage.GetValue();
                break;
            case StatType.BasicHP:
                value = basicHP.GetValue();
                break;
            case StatType.Armor:
                value = armor.GetValue();
                break;
            case StatType.Evasion:
                value = evasion.GetValue();
                break;
            default:
                break;
        }

        return value;
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
        int totalCriticalChange = criticalChancePercentage.GetValue() + agility.GetValue();
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
