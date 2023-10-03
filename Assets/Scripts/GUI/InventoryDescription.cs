using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDescription : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text desc;

    public void Awake()
    {
        
    }

    public void ResetDesc()
    {
        this.title.text = "";
        this.desc.text = "";
    }

    public void SetDesc(string itemName, string itemDesc)
    {
        this.title.text = itemName;
        this.desc.text = itemDesc;
    }
}
