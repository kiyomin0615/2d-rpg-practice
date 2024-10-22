using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public Dictionary<string, int> inventory;

    public GameData()
    {
        this.inventory = new Dictionary<string, int>();
    }
}
