using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class EquippedMeleeSlot : MonoBehaviour
    {
        private InventoryItem equippedItem;
        private Image spriteImg;
        private bool empty;

        private void Start()
        {
            spriteImg = this.GetComponent<Image>();
            empty = true;
            ResetData();
        }

        private void Awake()
        {
            //ResetData();
        }

        public void ResetData()
        {
            gameObject.SetActive(false);
            empty = true;
        }

        public void SetData(EquippablesSO pEquippedItem)
        {
            this.equippedItem.item = pEquippedItem;
            this.spriteImg.sprite = pEquippedItem.ItemSprite;
            gameObject.SetActive(true);
            this.empty = false;
        }

        public InventoryItem GetData()
        {
            return equippedItem;
        }
    }
}