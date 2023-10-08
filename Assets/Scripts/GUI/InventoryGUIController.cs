using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryGUIController : MonoBehaviour
    {
        [SerializeField] private InventoryPage inventoryPage;
        [SerializeField] private InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        private void Start()
        {
            PrepareInventoryUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryChanged += UpdateInventoryUI;

            foreach (InventoryItem i_item in initialItems)
            {
                if (i_item.IsEmpty)
                {
                    continue;
                }
                inventoryData.AddItem(i_item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryPage.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryPage.UpdateData(item.Key, item.Value.item.ItemSprite, item.Value.qty);
            }
        }

        private void PrepareInventoryUI()
        {
            inventoryPage.InitializeInventory(inventoryData.Size);
            inventoryPage.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryPage.OnItemActionRequested += HandleItemActionRequest;
            inventoryPage.OnStartDragging += HandleDragging;
            inventoryPage.OnSwapItems += HandleSwapping;
        }

        private void HandleSwapping(int itemIndex1, int itemIndex2)
        {
            inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                return;
            }
            inventoryPage.CreateDraggedItem(inventoryItem.item.ItemSprite, inventoryItem.qty);

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
}