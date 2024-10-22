using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public SerializableDictionary<string, int> inventory;
    public List<string> equipmentIdList;

    public GameData()
    {
        this.inventory = new SerializableDictionary<string, int>();
        this.equipmentIdList = new List<string>();
    }
}
