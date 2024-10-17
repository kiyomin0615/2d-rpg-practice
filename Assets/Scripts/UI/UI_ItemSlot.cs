using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemCountText;

    public Item item;

    public virtual void ClearItemSlotUI()
    {
        this.item = null;

        itemImage.color = Color.clear;
        itemImage.sprite = null;
        itemCountText.text = "";
    }

    public void UpdateItemSlotUI(Item item)
    {
        this.item = item;

        if (itemImage != null)
        {
            itemImage.color = Color.white;
            itemImage.sprite = item.itemData.itemIcon;

            if (item.count > 1)
            {
                itemCountText.text = item.count.ToString();
            }
            else
            {
                itemCountText.text = "";
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.itemData == null)
            return;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            ItemManager.instance.RemoveItem(item.itemData);
            return;
        }

        if (item.itemData.itemType == ItemType.Equipment)
        {
            ItemManager.instance.Equip(item.itemData as EquipmentData);
        }
    }
}
