using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 6;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryChanged;

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public int AddItem(ItemSO pItem, int pQty)
        {
            if (!pItem.IsStackable)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while (pQty > 0 && !IsInventoryFull())
                    {
                        pQty -= AddItemToFirstFreeSlot(pItem, 1);
                        pQty--;
                    }
                    InformAboutChange();
                    return pQty;
                }
            }

            pQty = AddStackableItem(pItem, pQty);
            InformAboutChange();
            return pQty;
        }

        private int AddItemToFirstFreeSlot(ItemSO pItem, int pQty)
        {
            InventoryItem nonStackableItem = new InventoryItem
            {
                item = pItem,
                qty = pQty
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = nonStackableItem;
                    return pQty;
                }
            }
            return 0;
        }

        private bool IsInventoryFull() => !(inventoryItems.Where(item => item.IsEmpty).Any());

        public int AddStackableItem(ItemSO pItem, int pQty)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty) continue;
                if (inventoryItems[i].item.ID == pItem.ID)
                {
                    int maxAmountPossible = inventoryItems[i].item.MaxStackSize - inventoryItems[i].qty;

                    if (pQty > maxAmountPossible)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQty(inventoryItems[i].item.MaxStackSize);
                        pQty -= maxAmountPossible;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQty(inventoryItems[i].qty + pQty);
                        InformAboutChange();
                        return 0;
                    }
                }
            }

            while (pQty > 0 && !IsInventoryFull())
            {
                int newQty = Mathf.Clamp(pQty, 0, pItem.MaxStackSize);
                pQty -= newQty;
                AddItemToFirstFreeSlot(pItem, newQty); 
            }
            return pQty;
        }

        public void AddItem(InventoryItem pItemObj)
        {
            AddItem(pItemObj.item, pItemObj.qty);
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnVal = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    continue;
                }
                returnVal[i] = inventoryItems[i];
            }
            return returnVal;
        }

        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            InventoryItem tempSwapItem = inventoryItems[itemIndex1];
            inventoryItems[itemIndex1] = inventoryItems[itemIndex2];
            inventoryItems[itemIndex2] = tempSwapItem;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryChanged?.Invoke(GetCurrentInventoryState());
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int qty;
        public ItemSO item;

        public bool IsEmpty => item == null;

        public InventoryItem(ItemSO pItem, int pQty)
        {
            qty = pQty;
            item = pItem;
        }

        public InventoryItem ChangeQty(int newQty)
        {
            return new InventoryItem
            {
                item = this.item,
                qty = newQty,
            };
        }

        public static InventoryItem GetEmptyItem()
            => new InventoryItem
            {
                item = null,
                qty = 0
            };

    }
}