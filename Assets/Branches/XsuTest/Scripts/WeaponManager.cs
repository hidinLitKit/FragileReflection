using FragileReflection;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace WeaponSystem
{
    public static class WeaponManager
    {
        public static Weapon currentWeapon;

        public static void ChangeWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            GameEvents.ChangeWeapon();
        }
    }
}
