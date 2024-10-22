using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Item,
    Equipment,
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public string itemId;
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;

    [Range(0f, 1f)]
    public float dropChance = 0.4f;

    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
    }
}
