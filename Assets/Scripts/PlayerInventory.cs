using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    [SerializeField] private Canvas inventory;
    private bool isDisplayed;
    // Start is called before the first frame update
    void Start()
    {
        inventory.enabled = false;
        isDisplayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!isDisplayed)
            {
                inventory.enabled = true;
                isDisplayed = true;
            }
            else
            {
                inventory.enabled = false;
                isDisplayed = false;
            }
        }
    }
}
