using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] ItemData itemData;
    
    [SerializeField] Vector2 velocity;

    public void SetupItemData(ItemData itemData)
    {
        this.itemData = itemData;

        RenderItemObject();

        rb = GetComponent<Rigidbody2D>();

        int xVelocity = Random.Range(-5, 5);
        velocity = new Vector2(xVelocity, 10f);

        rb.velocity = velocity;
    }

    public void PickupItem()
    {
        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }

    void RenderItemObject()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemData.itemIcon;

        gameObject.name = "Item Object - " + itemData.itemName;
    }
}
