using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace WeaponSystem
{
    public class Weapon : MonoBehaviour
    {
        public ScriptableWeapon WeaponType { get
            {
                return _weaponType;
            } 
        }
        [SerializeField] private ScriptableWeapon _weaponType;
    }
}
