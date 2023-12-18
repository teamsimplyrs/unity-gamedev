using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : WeaponHandler
{
    private void Start()
    {
        weapon.PerformAction(this.gameObject);
    }

    public override void SetWeapon(EquippablesSO weaponSO, List<ItemParameter> itemState, List<ItemParameter> itemParameters)
    {
        weapon = weaponSO;
        weaponCurrentState = new List<ItemParameter>(itemState);
        weaponParameters = new List<ItemParameter>(itemParameters);
        //equippedMeleeSlot.SetData(weaponSO.ItemSprite);
    }
}
