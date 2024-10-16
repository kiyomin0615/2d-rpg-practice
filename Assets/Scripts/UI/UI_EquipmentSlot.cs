using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType equipmentType;

    private void OnValidate()
    {
        gameObject.name = $"EquipmentSlot - {equipmentType.ToString()}";
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        Inventory.instance.UnEquip(item.itemData as EquipmentData);
        ClearItemSlotUI();
    }
}
