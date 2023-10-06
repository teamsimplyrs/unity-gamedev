using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
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

    public void Deselect()
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

    public void OnPointerClick(PointerEventData ptrData)
    {
        if (ptrData.button == PointerEventData.InputButton.Right)
        {
            OnItemAltClicked?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // This method exists as a result of there being a necessity to implement the IDragHandler interface
        // when IBeginDragHandler and IEndDragHandler are implemented.
        // This method is to be left empty as it is. (what a Drag fr fr)
        // -shlok
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty) return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

}
