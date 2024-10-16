using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask,
}

[CreateAssetMenu(fileName = "New Equipment Data", menuName = "Data/Equipment")]
public class EquipmentData : ItemData
{
    public EquipmentType equipmentType;

    public int strength;
    public int agility;
    public int vitality;

    public int damage;
    public int criticalChange;
    public int criticalDamagePercentage;

    public int basicHP;
    public int armor;
    public int evasion;

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.vitality.AddModifier(vitality);

        playerStats.damage.AddModifier(damage);
        playerStats.criticalChange.AddModifier(criticalChange);
        playerStats.criticalDamagePercentage.AddModifier(criticalDamagePercentage);

        playerStats.basicHP.AddModifier(basicHP);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.vitality.RemoveModifier(vitality);

        playerStats.damage.RemoveModifier(damage);
        playerStats.criticalChange.RemoveModifier(criticalChange);
        playerStats.criticalDamagePercentage.RemoveModifier(criticalDamagePercentage);

        playerStats.basicHP.RemoveModifier(basicHP);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
    }
}
