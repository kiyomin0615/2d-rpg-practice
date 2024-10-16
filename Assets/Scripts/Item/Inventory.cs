using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Item> inventory = new List<Item>();
    public Dictionary<ItemData, Item> dictionary;

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
    }

    void Update()
    {

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
    }
}
