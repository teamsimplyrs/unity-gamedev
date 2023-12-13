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

    public override void SetWeapon(EquippablesSO weaponSO, List<ItemParameter> itemState)
    {
        weapon = weaponSO;
        weaponCurrentState = new List<ItemParameter>(itemState);
        //equippedMeleeSlot.SetData(weaponSO.ItemSprite);
    }
}
