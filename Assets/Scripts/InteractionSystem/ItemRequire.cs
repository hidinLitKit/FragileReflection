using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class ItemRequire : Interactable
    {
        [SerializeField] private KeyObject key;
        private bool isLocked = true;
        public KeyObject Key => key;
        public bool IsLocked => isLocked;
        public void ChangeLockState()
        {
            isLocked = !isLocked;
            Debug.Log(isLocked);
        }

        public override string GetDescription()
        {
            if (isLocked) return "закрыто";
            return "открыто";
        }

        public override void Interact()
        {
            if (GameInventory.instance.inventory.ContainsObject(key))
            {
                GameEvents.UseKey(key);
            }
        }
    }
}
