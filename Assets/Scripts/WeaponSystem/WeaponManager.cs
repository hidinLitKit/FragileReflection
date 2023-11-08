using FragileReflection;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace WeaponSystem
{
    public static class WeaponManager
    {
        public static List<Weapon> weapons = new List<Weapon>();
        public static Weapon currentWeapon = null;
        public static bool isWeaponEquiped = false;


        public static void SwitchWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            GameEvents.ChangeWeapon();
        }
        public static void AddWeapon(Weapon weapon)
        {
            weapons.Add(weapon);
        }
        public static void EquipWeapon(int weaponIndex)
        {
            currentWeapon = weapons[weaponIndex];
            GameEvents.ChangeWeapon();
        }
        public static void UnEquipWeapon()
        {
            currentWeapon = null;
            GameEvents.ChangeWeapon();
        }
        //избавляться от оружия не планируется
    }
}
