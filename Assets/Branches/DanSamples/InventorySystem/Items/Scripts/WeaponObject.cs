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
        public override void Examine()
        {
            throw new System.NotImplementedException();
        }

        public override void Use()
        {
            WeaponManager.SwitchWeapon(weaponType);
        }
    }
}
