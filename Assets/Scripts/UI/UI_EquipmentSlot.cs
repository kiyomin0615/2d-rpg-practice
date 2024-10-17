using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType equipmentType;

    Color originalColor;
    Sprite originalSprite;

    private void Start() {
        originalColor = itemImage.color;
        originalSprite = itemImage.sprite;
    }

    private void OnValidate()
    {
        gameObject.name = $"UI_EquipmentSlot - {equipmentType.ToString()}";
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.itemData == null)
            return;

        ItemManager.instance.UnEquip(item.itemData as EquipmentData);
        ClearItemSlotUI();
    }

    public override void ClearItemSlotUI()
    {
        base.ClearItemSlotUI();

        itemImage.color = originalColor;
        itemImage.sprite = originalSprite;
    }
}
