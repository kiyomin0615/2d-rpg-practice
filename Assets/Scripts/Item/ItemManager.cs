using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ItemManager : MonoBehaviour, ISaveManager
{
    public static ItemManager instance;

    public List<Item> inventory = new List<Item>();
    public Dictionary<ItemData, Item> dictionary;

    public List<Item> equipments = new List<Item>();
    public Dictionary<EquipmentData, Item> equipmentDictionary;

    [Header("UI")]
    [SerializeField] Transform itemSlotsParentTransform;
    [SerializeField] UI_ItemSlot[] itemSlots;
    [SerializeField] Transform equipmentSlotsParentTransform;
    [SerializeField] UI_EquipmentSlot[] equipmentSlots;
    [SerializeField] Transform statSlotsParentTransform;
    [SerializeField] UI_StatSlot[] statSlots;

    List<Item> loadedItems = new List<Item>();
    List<EquipmentData> loadedEquipments = new List<EquipmentData>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Init();
        InitInventory();
        InitEquipments();
    }

    void Init()
    {
        inventory = new List<Item>();
        equipments = new List<Item>();

        dictionary = new Dictionary<ItemData, Item>();
        equipmentDictionary = new Dictionary<EquipmentData, Item>();

        itemSlots = itemSlotsParentTransform.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlots = equipmentSlotsParentTransform.GetComponentsInChildren<UI_EquipmentSlot>();
        statSlots = statSlotsParentTransform.GetComponentsInChildren<UI_StatSlot>();
    }

    void InitInventory()
    {
        if (loadedItems != null || loadedItems.Count > 0)
        {
            foreach (Item item in loadedItems)
            {
                for (int i = 0; i < item.count; i++)
                {
                    AddItem(item.itemData);
                }
            }
        }
    }

    void InitEquipments()
    {
        if (loadedEquipments != null || loadedEquipments.Count > 0)
        {
            foreach (EquipmentData equipmentData in loadedEquipments)
            {
                Equip(equipmentData);
            }
        }
    }

    public void AddItem(ItemData itemData)
    {
        if (IsInventoryFull())
            return;

        if (dictionary.TryGetValue(itemData, out Item item))
        {
            item.IncreaseItemStack();
        }
        else
        {
            Item newItem = new Item(itemData);
            inventory.Add(newItem);
            dictionary.Add(itemData, newItem);
        }

        UpdateSlotsUI();
    }

    public void RemoveItem(ItemData itemData)
    {
        if (dictionary.TryGetValue(itemData, out Item item))
        {
            if (item.count <= 1)
            {
                inventory.Remove(item);
                dictionary.Remove(itemData);
            }
            else
            {
                item.DecreaseItemStack();
            }
        }

        UpdateSlotsUI();
    }

    public void Equip(EquipmentData equipmentData)
    {
        Item equipment = new Item(equipmentData);

        EquipmentData oldEquipmentData = null;

        foreach (KeyValuePair<EquipmentData, Item> pair in equipmentDictionary)
        {
            if (pair.Key.equipmentType == equipmentData.equipmentType)
                oldEquipmentData = pair.Key;
        }

        // if the same type of equipment is already equipped, replace it
        if (oldEquipmentData != null)
        {
            UnEquip(oldEquipmentData);
        }

        equipments.Add(equipment);
        equipmentDictionary.Add(equipmentData, equipment);
        equipmentData.AddModifiers();
        RemoveItem(equipmentData);
    }

    public void UnEquip(EquipmentData oldEquipmentData)
    {
        if (equipmentDictionary.TryGetValue(oldEquipmentData, out Item value))
        {
            equipments.Remove(value);
            equipmentDictionary.Remove(oldEquipmentData);
        }

        oldEquipmentData.RemoveModifiers();

        AddItem(oldEquipmentData);
    }

    public bool CanCraft(EquipmentData equipmentData, List<Item> requirements)
    {
        List<Item> listToRemoveFromItemManager = new List<Item>();

        for (int i = 0; i < requirements.Count; i++)
        {
            if (dictionary.TryGetValue(requirements[i].itemData, out Item value))
            {
                if (value.count < requirements[i].count)
                {
                    Debug.Log("Not enough requirements.");
                    return false;
                }
                else
                {
                    listToRemoveFromItemManager.Add(value);
                }
            }
            else
            {
                Debug.Log("Not enough requirements.");
                return false;
            }
        }

        for (int i = 0; i < listToRemoveFromItemManager.Count; i++)
        {
            RemoveItem(listToRemoveFromItemManager[i].itemData);
        }

        AddItem(equipmentData);
        return true;
    }

    public bool IsInventoryFull()
    {
        if (inventory.Count >= itemSlots.Length)
            return true;
        else
            return false;
    }

    void UpdateSlotsUI()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].ClearItemSlotUI();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            itemSlots[i].UpdateItemSlotUI(inventory[i]);
        }

        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            foreach (KeyValuePair<EquipmentData, Item> pair in equipmentDictionary)
            {
                if (pair.Key.equipmentType == equipmentSlots[i].equipmentType)
                    equipmentSlots[i].UpdateItemSlotUI(pair.Value);
            }
        }

        for (int i = 0; i < statSlots.Length; i++)
        {
            statSlots[i].UpdateStatSlotUI();
        }
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.inventory.Clear();
        gameData.equipmentIdList.Clear();

        foreach (KeyValuePair<ItemData, Item> pair in dictionary)
        {
            gameData.inventory.Add(pair.Key.itemId, pair.Value.count); // id - count
        }

        foreach (KeyValuePair<EquipmentData, Item> pair in equipmentDictionary)
        {
            gameData.equipmentIdList.Add(pair.Key.itemId);
        }
    }

    public void LoadData(GameData gameData)
    {
        if (gameData == null)
            return;

        foreach (KeyValuePair<string, int> pair in gameData.inventory)
        {
            foreach (ItemData itemData in GetItemDatabase())
            {
                if (itemData != null && itemData.itemId == pair.Key)
                {
                    Item item = new Item(itemData);
                    item.count = pair.Value;
                    loadedItems.Add(item);
                }
            }
        }

        foreach (string equipmentId in gameData.equipmentIdList)
        {
            foreach (EquipmentData equipmentData in GetEquipmentDatabase())
            {
                if (equipmentData != null && equipmentData.itemId == equipmentId)
                {
                    loadedEquipments.Add(equipmentData);
                }
            }
        }
    }

    List<ItemData> GetItemDatabase()
    {
        List<ItemData> itemDatabase = new List<ItemData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Data/Item" }); // GUID
        foreach (string assetName in assetNames)
        {
            string itemPath = AssetDatabase.GUIDToAssetPath(assetName);
            ItemData itemData = AssetDatabase.LoadAssetAtPath<ItemData>(itemPath);
            itemDatabase.Add(itemData);
        }

        return itemDatabase;
    }

    List<EquipmentData> GetEquipmentDatabase()
    {
        List<EquipmentData> equipmentDatabase = new List<EquipmentData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Data/Equipment" }); // GUID
        foreach (string assetName in assetNames)
        {
            string itemPath = AssetDatabase.GUIDToAssetPath(assetName);
            EquipmentData equipmentData = AssetDatabase.LoadAssetAtPath<EquipmentData>(itemPath);
            equipmentDatabase.Add(equipmentData);
        }

        return equipmentDatabase;
    }
}
