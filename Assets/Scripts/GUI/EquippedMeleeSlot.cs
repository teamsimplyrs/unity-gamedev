using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class EquippedMeleeSlot : MonoBehaviour
    {
        private Image img;
        private bool empty;

        private void Start()
        {
            img = GetComponent<Image>();
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

        public void SetData(Sprite equippingItemSprite)
        {
            this.img.sprite = equippingItemSprite;
            gameObject.SetActive(true);
            this.empty = false;
        }
    }
}