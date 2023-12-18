using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : WeaponHandler
{
    public override void SetWeapon(EquippablesSO weaponSO, List<ItemParameter> itemState, List<ItemParameter> itemParameters)
    {
        if (this.weapon != null)
        {
            inventoryData.AddItem(this.weapon, 1, weaponCurrentState);
        }

        this.weapon = weaponSO;
        this.weaponCurrentState = new List<ItemParameter>(itemState);
        this.weaponParameters = new List<ItemParameter>(itemParameters);
        equippedMeleeSlot.SetData(weaponSO);
    }
}
