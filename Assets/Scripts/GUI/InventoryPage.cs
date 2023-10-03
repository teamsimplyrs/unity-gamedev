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

    public Sprite sprite;
    public int qty;
    public string title, desc;

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

    private void HandleShowItemActions(InventoryItemSlot obj)
    {
        
    }

    private void HandleSwap(InventoryItemSlot obj)
    {
        
    }

    private void HandleEndDrag(InventoryItemSlot obj)
    {
        mouseFollower.Toggle(false);
    }

    private void HandleBeginDrag(InventoryItemSlot obj)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, qty);
    }

    private void HandleItemSelection(InventoryItemSlot obj)
    {
        itemDesc.SetDesc(title, desc);
        listItemSlots[0].Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDesc.ResetDesc();

        listItemSlots[0].SetData(sprite, qty);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
}
