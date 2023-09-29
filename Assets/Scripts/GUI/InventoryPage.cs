using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField] private InventoryItemSlot itemSlotPrefab;
    [SerializeField] private RectTransform contentPanel;

    List<InventoryItemSlot> listItemSlots = new List<InventoryItemSlot>();

    public void InitializeInventory(int invSize)
    {
        for (int i = 0; i < invSize; i++)
        {
            InventoryItemSlot slot = Instantiate(itemSlotPrefab, Vector3.zero, Quaternion.identity);
            slot.transform.SetParent(contentPanel);
            slot.transform.localScale = Vector3.one;
            Debug.Log(contentPanel.transform.localScale);
            listItemSlots.Add(slot);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
}
