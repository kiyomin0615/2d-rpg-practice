using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ItemSlot : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemCountText;

    public Item item;

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
}
