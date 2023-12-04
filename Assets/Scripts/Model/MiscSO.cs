using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class MiscSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "";

        public AudioClip ActionSound { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            return true;
        }
    }
}
