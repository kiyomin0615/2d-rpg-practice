using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    [SerializeField] Stat stat;
    [SerializeField] TextMeshProUGUI statNameText;
    [SerializeField] TextMeshProUGUI statValueText;

    void Start()
    {
        UpdateStatSlotUI();
    }

    void Update()
    {

    }

    public void UpdateStatSlotUI()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        bool isValid = ConvertStatNameToStatType(out StatType statType);

        if (isValid)
        {
            int statValue = playerStats.GetStatValue(statType);
            statValueText.text = statValue.ToString();
        }
        else
        {
            Debug.LogError("Invalid Stat Type.");
        }
    }

    bool ConvertStatNameToStatType(out StatType statType)
    {
        string statName = statNameText.text.Replace(" ", "");
        return Enum.TryParse(statName, true, out statType);
    }
}
