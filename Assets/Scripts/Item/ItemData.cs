using UnityEngine;

public enum ItemType
{
    Item,
    Equipment,
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;

    [Range(0f, 1f)]
    public float dropChance = 0.4f;
}
