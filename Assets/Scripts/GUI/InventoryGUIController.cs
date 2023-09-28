using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGUIController : MonoBehaviour
{
    [SerializeField] private InventoryPage inventoryPage;

    public int inventorySize = 6;

    private void Start()
    {
        inventoryPage.InitializeInventory(inventorySize);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!inventoryPage.isActiveAndEnabled) // if inventory GUI is not active
            {
                inventoryPage.Show();
            }
            else
            {
                inventoryPage.Hide();
            }
        }
    }
}
