using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EquippablesSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";

        [field: SerializeField]
        public bool HasProjectile;

        [field: SerializeField]
        public AudioClip ActionSound => throw new System.NotImplementedException();

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            PlayerWeapon weaponSystem = character.GetComponent<PlayerWeapon>();
            if (weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, itemState == null ?
                    DefaultParametersList : itemState);
                SwordAnimation swordAnim = character.GetComponentInChildren<SwordAnimation>(true);
                if (swordAnim != null)
                {
                    swordAnim.UpdateEquippedSword(character);
                    Debug.Log("sword anim is not null");
                }
                return true;
            }
            
            return false;
        }
    }
}