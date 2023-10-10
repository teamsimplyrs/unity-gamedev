using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            int remainder = inventoryData.AddItem(item.InventoryItem, item.Qty);
            if (remainder == 0)
            {
                item.DestroyItem();
            }
            else
            {
                item.Qty = remainder;
            }
        }

        Debug.Log("collision occurred somewhere.");
        Debug.Log("Collision source object: " + collision.gameObject);
    }
}
