using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Inventory
{
    public class InventoryGUIController : MonoBehaviour
    {
        [SerializeField] private InventoryPage inventoryPage;
        [SerializeField] private InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        [SerializeField] private AudioClip dropClip;
        [SerializeField] private AudioSource audioSource;

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
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty) return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                inventoryPage.ShowItemAction(itemIndex);
                inventoryPage.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryPage.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.qty));
            }
        }

        private void DropItem(int itemIndex, int qty)
        {
            inventoryData.RemoveItem(itemIndex, qty);
            inventoryPage.ResetSelection();
            if (audioSource != null)
            {
                audioSource.PlayOneShot(dropClip);
            }
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty) return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState);
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(itemAction.ActionSound);
                }
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                {
                    inventoryPage.ResetSelection();
                    inventoryPage.ResetDescription();
                    inventoryPage.HideItemAction();
                }
            }
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
            string description = PrepareDescription(invItem);
            inventoryPage.UpdateDescription(itemIndex, item.Name, description);
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                ItemParameter param = inventoryItem.itemState[i];
                switch (param.itemParameter.ParameterName)
                {
                    case "Durability":
                        sb.AppendLine(
                            $"{param.itemParameter.ParameterName}" +
                            $": {param.value} / " +
                            $"{inventoryItem.item.DefaultParametersList[i].value}"
                        );
                        break;
                    case "Melee Damage":
                        sb.AppendLine(
                            $"{param.itemParameter.ParameterName}" +
                            $": {param.value}"
                        );
                        break;
                    case "Critical Chance":
                        sb.AppendLine(
                            $"{param.itemParameter.ParameterName}" +
                            $": {param.value}%"
                        );
                        break;
                    case "Critical Multiplier":
                        sb.AppendLine(
                            $"{param.itemParameter.ParameterName}" +
                            $": +{param.value}x"
                        );
                        break;
                }
                
            }

            return sb.ToString();
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