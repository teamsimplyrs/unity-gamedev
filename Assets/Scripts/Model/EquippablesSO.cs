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
        public bool HasProjectile { get; private set; }

        [field: SerializeField]
        public ProjectileSO WeaponProjectile { get; private set; }

        [field: SerializeField]
        public AudioClip ActionSound { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            WeaponHandler weaponSystem = character.GetComponent<WeaponHandler>();
            if (weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, itemState == null ? DefaultParametersList : itemState, DefaultParametersList);
                SwordAnimation swordAnim = character.GetComponentInChildren<SwordAnimation>(true);
                if (swordAnim != null)
                {
                    swordAnim.UpdateEquippedSword(character);
                    //Debug.Log("sword anim is not null");
                }
                return true;
            }
            return false;
        }
    }
}