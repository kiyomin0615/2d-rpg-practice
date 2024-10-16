using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public ItemData itemData;
    public int count;
    
    public Item(ItemData itemData)
    {
        this.itemData = itemData;
        this.count = 1;
    }

    public void IncreaseItemStack()
    {
        count++;
    }

    public void DecreaseItemStack()
    {
        count--;
    }
}
