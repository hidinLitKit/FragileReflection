using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;
namespace FragileReflection
{
    [CreateAssetMenu(fileName = "New Ammo", menuName = "ScriptableObjects/Inventory System/Items/Ammo")]
    public class AmmoObject : ItemObject
    {
        public ScriptableWeapon weaponType;
        public override void Examine()
        {

        }

        public override void Use()
        {

        }
    }
}
