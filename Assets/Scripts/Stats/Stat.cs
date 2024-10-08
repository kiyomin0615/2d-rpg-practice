using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] int baseValue;
    List<int> modifiers = new List<int>();

    public int GetValue() {
        int finalValue = baseValue;

        foreach (int modifier in modifiers) {
            finalValue += modifier;
        }

        return finalValue;
    }

    public void AddModifier(int modifier) {
        modifiers.Add(modifier);
    }

    public void RemoveModifier(int index) {
        modifiers.RemoveAt(index);
    }
}
