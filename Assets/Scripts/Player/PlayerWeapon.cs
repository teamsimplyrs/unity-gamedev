using Inventory.Model;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private EquippablesSO weapon;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> weaponParameters, weaponCurrentState;

    [SerializeField]
    private EquippedMeleeSlot equippedMeleeSlot;

    public void SetWeapon(EquippablesSO weaponSO, List<ItemParameter> itemState)
    {
        if (weapon != null)
        {
            inventoryData.AddItem(weapon, 1, weaponCurrentState);
        }

        this.weapon = weaponSO;
        this.weaponCurrentState = new List<ItemParameter>(itemState);
        equippedMeleeSlot.SetData(weaponSO.ItemSprite);
        ModifyParameters();
    }

    public EquippablesSO GetWeapon()
    {
        return this.weapon;
    }

    private void ModifyParameters()
    {
        foreach (var param in weaponParameters)
        {
            if (weaponCurrentState.Contains(param))
            {
                int index = weaponCurrentState.IndexOf(param);
                float newValue = weaponCurrentState[index].value + param.value;
                weaponCurrentState[index] = new ItemParameter
                {
                    itemParameter = param.itemParameter,
                    value = newValue
                };

            }
        }
    }

}
