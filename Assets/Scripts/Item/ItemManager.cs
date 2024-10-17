using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
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

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        inventory = new List<Item>();
        equipments = new List<Item>();

        dictionary = new Dictionary<ItemData, Item>();
        equipmentDictionary = new Dictionary<EquipmentData, Item>();

        itemSlots = itemSlotsParentTransform.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlots = equipmentSlotsParentTransform.GetComponentsInChildren<UI_EquipmentSlot>();
        statSlots = statSlotsParentTransform.GetComponentsInChildren<UI_StatSlot>();
    }

    public void AddItem(ItemData itemData)
    {
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
}
