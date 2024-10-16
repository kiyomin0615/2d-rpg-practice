using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Item> inventory = new List<Item>();
    public Dictionary<ItemData, Item> dictionary;

    [Header("UI")]
    [SerializeField] Transform itemSlotsParentTransform;
    [SerializeField] UI_ItemSlot[] itemSlots;


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
        dictionary = new Dictionary<ItemData, Item>();

        itemSlots = itemSlotsParentTransform.GetComponentsInChildren<UI_ItemSlot>();
    }

    void UpdateItemSlotsUI()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            itemSlots[i].UpdateItemSlotUI(inventory[i]);
        }
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

    public void RemoveIte(ItemData itemData)
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
}
