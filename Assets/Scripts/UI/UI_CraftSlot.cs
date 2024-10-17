using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{
    private void OnEnable()
    {
        UpdateItemSlotUI(item);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        EquipmentData craftData = item.itemData as EquipmentData;

        ItemManager.instance.CanCraft(craftData, craftData.requirements);
    }
}
