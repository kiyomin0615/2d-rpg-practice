using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public SerializableDictionary<string, int> inventory;
    public List<string> equipmentIdList;
    public SerializableDictionary<string, bool> checkpointDict;
    public string closestCheckpointUID;
    public SerializableDictionary<string, float> volumeSettingDict;

    public GameData()
    {
        this.inventory = new SerializableDictionary<string, int>();
        this.equipmentIdList = new List<string>();
        this.checkpointDict = new SerializableDictionary<string, bool>();
        this.closestCheckpointUID = string.Empty;
        this.volumeSettingDict = new SerializableDictionary<string, float>();
    }
}
