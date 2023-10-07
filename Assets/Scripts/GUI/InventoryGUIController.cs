using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGUIController : MonoBehaviour
{
    [SerializeField] private InventoryPage inventoryPage;
    [SerializeField] private InventorySO inventoryData;


    private void Start()
    {
        PrepareInventory();
        /*inventoryData.Initialize();*/
    }

    private void PrepareInventory()
    {
        inventoryPage.InitializeInventory(inventoryData.Size);
        this.inventoryPage.OnDescriptionRequested += HandleDescriptionRequest;
        this.inventoryPage.OnItemActionRequested += HandleItemActionRequest;
        this.inventoryPage.OnStartDragging += HandleDragging;
        this.inventoryPage.OnSwapItems += HandleSwapping;
    }

    private void HandleSwapping(int itemIndex1, int itemIndex2)
    {
        
    }

    private void HandleDragging(int itemIndex)
    {
        
    }

    private void HandleItemActionRequest(int itemIndex)
    {
        
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem invItem = inventoryData.GetItemAt(itemIndex);
        if (invItem.IsEmpty)
        {
            inventoryPage.ResetSelection();
            return;
        }
        ItemSO item = invItem.item;
        inventoryPage.UpdateDescription(itemIndex, item.Name, item.Description);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!inventoryPage.isActiveAndEnabled) // if inventory GUI is not active
            {
                inventoryPage.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryPage.UpdateData(item.Key, item.Value.item.ItemSprite, item.Value.qty);
                }
            }
            else
            {
                inventoryPage.Hide();
            }
        }
    }
}
