using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectTrigger : MonoBehaviour
{
    ItemObject itemObject;

    private void Awake()
    {
        itemObject = GetComponentInParent<ItemObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && !player.stats.isDead)
        {
            itemObject.PickupItem();
        }
    }
}
