using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;
namespace FragileReflection
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Inventory System/Items/Weapon")]
    public class WeaponObject : ItemObject
    {
        public ScriptableWeapon weaponType;
        [SerializeField] private AmmoObject ammo;
        private int _ammoID;
        public override void Examine()
        {
            throw new System.NotImplementedException();
        }

        public override void Use()
        {
            WeaponManager.SwitchWeapon(weaponType);
        }
        public int AmmoLeft()
        {
            _ammoID = ammo.ID;
            foreach (InventorySlot slot in GameInventory.instance.inventory.Container.Items)
            {
                if (slot.ID == _ammoID) return slot.amount;
            }
            return 0;

        }
        public void AmmoSet(int minus)
        {
            _ammoID = ammo.ID;
            foreach (InventorySlot slot in GameInventory.instance.inventory.Container.Items)
            {
                if (slot.ID == _ammoID)
                {
                    slot.amount -= minus;
                    if (slot.amount < 0) slot.amount = 0;
                }
            }
        }
    }
}
