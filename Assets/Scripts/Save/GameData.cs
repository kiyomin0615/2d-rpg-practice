using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public SerializableDictionary<string, int> inventory;

    public GameData()
    {
        this.inventory = new SerializableDictionary<string, int>();
    }
}
