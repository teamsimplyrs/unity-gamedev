using Inventory.Model;
using Inventory.UI;
using System;
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
    public List<ItemParameter> weaponParameters, weaponCurrentState;

    [SerializeField]
    private EquippedMeleeSlot equippedMeleeSlot;

    public void SetWeapon(EquippablesSO weaponSO, List<ItemParameter> itemState)
    {
        if (this.weapon != null)
        {
            // Debug.Log("Printing state during swap:");
            // Debug.Log(this.weapon);

            inventoryData.AddItem(this.weapon, 1, weaponCurrentState);
        }

        this.weapon = weaponSO;
        this.weaponCurrentState = new List<ItemParameter>(itemState);
        equippedMeleeSlot.SetData(weaponSO.ItemSprite);
    }

    public EquippablesSO GetWeapon()
    {
        return weapon;
    }

    private void ModifyParameters(String parameterName, float valueChange)
    {
        for(int i=0; i<weaponCurrentState.Count; i++)
        {
            ItemParameter itemParameter = weaponCurrentState[i];
            if (itemParameter.itemParameter.ParameterName == parameterName)
            {
                itemParameter.value = itemParameter.value + valueChange;
                weaponCurrentState[i] = itemParameter;
            }
        }

        /*foreach (var parameter in weaponParameters)
        {
            if (weaponCurrentState.Contains(parameter))
            {
                int index = weaponCurrentState.IndexOf(parameter);
                float newValue = weaponCurrentState[index].value + parameter.value;
                weaponCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue,
                };
            }
        }*/
    }

    public void ReduceDurability(float durabilityDecrease)
    {
        if (this.weapon != null)
        {
            ModifyParameters("Durability", -durabilityDecrease);
            for (int i = 0; i < weaponCurrentState.Count; i++)
            {
                ItemParameter itemParameter = weaponCurrentState[i];
                if (itemParameter.itemParameter.ParameterName == "Durability")
                {
                    if (itemParameter.value <= 0)
                    {
                        UnequipAndDestroy();
                    }
                }
            }
        }
    }

    public void UnequipAndDestroy()
    {
        weapon = null;
        weaponCurrentState = null;
        weaponParameters = null;
        equippedMeleeSlot.ResetData();
    }

}
