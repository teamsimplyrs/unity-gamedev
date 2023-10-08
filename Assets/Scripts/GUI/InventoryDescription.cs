using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Inventory.UI
{
    public class InventoryDescription : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text desc;
        [SerializeField] private RectTransform scrollview;

        public void Awake()
        {

        }

        public void ResetDesc()
        {
            scrollview.gameObject.SetActive(false);
        }

        public void SetDesc(string itemName, string itemDesc)
        {
            scrollview.gameObject.SetActive(true);
            title.text = itemName;
            desc.text = itemDesc;
        }
    }
}