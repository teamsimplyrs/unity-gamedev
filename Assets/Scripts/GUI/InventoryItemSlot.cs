using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImg;
    [SerializeField] private TMP_Text quantityDisp;

    [SerializeField] private Image selectBorder;

    public event Action<InventoryItemSlot> OnItemClicked, OnItemAltClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag;

    private bool empty = true;

    private void Awake()
    {
        ResetData();
        Deselect();
    }

    private void Deselect()
    {
        selectBorder.enabled = false;
    }

    private void ResetData()
    {
        this.itemImg.gameObject.SetActive(false);
        this.empty = true;
    }

    public void SetData(Sprite sprite, int qty)
    {
        this.itemImg.gameObject.SetActive(true);
        this.itemImg.sprite = sprite;
        this.quantityDisp.text = qty + "";
        this.empty = false;
    }

    public void Select()
    {
        selectBorder.enabled = true;
    }

    public void OnBeginDrag()
    {
        if (empty) return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnDrop()
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData ptrData = (PointerEventData)data;

        if (ptrData.button == PointerEventData.InputButton.Right)
        {
            OnItemAltClicked?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
