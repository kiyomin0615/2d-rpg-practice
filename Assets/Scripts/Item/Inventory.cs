using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Item> inventory = new List<Item>();
    public Dictionary<ItemData, Item> dictionary;

    public List<Item> equipments = new List<Item>();
    public Dictionary<EquipmentData, Item> equipmentDictionary;

    [Header("UI")]
    [SerializeField] Transform itemSlotsParentTransform;
    [SerializeField] UI_ItemSlot[] itemSlots;
    [SerializeField] Transform equipmentSlotsParentTransform;
    [SerializeField] UI_EquipmentSlot[] equipmentSlots;


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

        UpdateItemSlotsUI();
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

        UpdateItemSlotsUI();
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

    void UpdateItemSlotsUI()
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
    }

}
