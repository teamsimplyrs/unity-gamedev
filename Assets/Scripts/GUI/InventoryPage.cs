using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField] private InventoryItemSlot itemSlotPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private InventoryDescription itemDesc;
    [SerializeField] private MouseFollower mouseFollower;

    List<InventoryItemSlot> listItemSlots = new List<InventoryItemSlot>();

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
    public event Action<int, int> OnSwapItems;

    private int currentlyDraggedItemIndex = -1;

    private void Awake()
    {
        Hide();
        itemDesc.ResetDesc();
        mouseFollower.Toggle(false);
    }

    public void InitializeInventory(int invSize)
    {
        for (int i = 0; i < invSize; i++)
        {
            InventoryItemSlot slot = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity);
            slot.transform.SetParent(contentPanel);
            slot.transform.localScale = Vector3.one;
            listItemSlots.Add(slot);

            slot.OnItemClicked += HandleItemSelection;
            slot.OnItemBeginDrag += HandleBeginDrag;
            slot.OnItemEndDrag += HandleEndDrag;
            slot.OnItemDroppedOn += HandleSwap;
            slot.OnItemAltClicked += HandleShowItemActions;
        }
    }

    internal void UpdateDescription(int itemIndex, string name, string description)
    {
        itemDesc.SetDesc(name, description);
        DeselectAllItems();
        listItemSlots[itemIndex].Select();
    }

    public void UpdateData(int itemIndex, Sprite sprite, int qty)
    {
        if (listItemSlots.Count > itemIndex)
        {
            listItemSlots[itemIndex].SetData(sprite, qty);
        }
    }

    private void HandleShowItemActions(InventoryItemSlot pSlot)
    {
        
    }

    private void HandleSwap(InventoryItemSlot pSlot)
    {
        int index = listItemSlots.IndexOf(pSlot);
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
        return;
    }

    private void HandleEndDrag(InventoryItemSlot pSlot)
    {
        ResetDraggedItem();

    }

    private void HandleBeginDrag(InventoryItemSlot pSlot)
    {
        int index = listItemSlots.IndexOf(pSlot);
        if (index == -1) return;
        currentlyDraggedItemIndex = index;
        HandleItemSelection(pSlot);
        OnStartDragging?.Invoke(index);
    }

    public void CreateDraggedItem(Sprite sprite, int qty)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, qty);
    }

    private void HandleItemSelection(InventoryItemSlot pSlot)
    {
        int index = listItemSlots.IndexOf(pSlot);
        if (index == -1)
        {
            return;
        }
        OnDescriptionRequested?.Invoke(index);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void ResetSelection()
    {
        itemDesc.ResetDesc();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach (InventoryItemSlot itemSlot in listItemSlots)
        {
            itemSlot.Deselect();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }
    
}
