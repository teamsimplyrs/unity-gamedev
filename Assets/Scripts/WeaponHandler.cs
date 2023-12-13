using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    protected EquippablesSO weapon;

    [SerializeField]
    protected InventorySO inventoryData;

    [SerializeField]
    public List<ItemParameter> weaponParameters, weaponCurrentState;

    [SerializeField]
    protected EquippedMeleeSlot equippedMeleeSlot;

    public abstract void SetWeapon(EquippablesSO weaponSO, List<ItemParameter> itemState);

    public EquippablesSO GetWeapon()
    {
        return weapon;
    }

    private void ModifyParameters(String parameterName, float valueChange)
    {
        for (int i = 0; i < weaponCurrentState.Count; i++)
        {
            ItemParameter itemParameter = weaponCurrentState[i];
            if (itemParameter.itemParameter.ParameterName == parameterName)
            {
                itemParameter.value = itemParameter.value + valueChange;
                weaponCurrentState[i] = itemParameter;
            }
        }
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
