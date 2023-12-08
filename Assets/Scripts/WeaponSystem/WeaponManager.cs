using FragileReflection;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
namespace WeaponSystem
{
    public class WeaponManager: MonoBehaviour
    {
        public static WeaponManager instance = null;
        public List<Weapon> weapons = new List<Weapon>();
        public Weapon currentWeapon = null;
        public bool isWeaponEquiped = false;

        void Start()
        {
            if (instance == null) instance = this;
            else if (instance == this) Destroy(gameObject);
        }
        public void SwitchWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            GameEvents.ChangeWeapon();
        }
        public void SwitchWeapon(ScriptableWeapon weapon)
        {
            for(int i = 0; i<weapons.Count;i++)
            {
                if (weapons[i].WeaponType == weapon) currentWeapon = weapons[i];
            }
            GameEvents.ChangeWeapon();
        }
        public void AddWeapon(Weapon weapon)
        {
            weapons.Add(weapon);
        }
        public void EquipWeapon(int weaponIndex)
        {
            currentWeapon = weapons[weaponIndex];
            GameEvents.ChangeWeapon();
        }
        public void UnEquipWeapon()
        {
            currentWeapon = null;
            GameEvents.ChangeWeapon();
        }
        public int GetAmmo(ScriptableWeapon type)
        {
            return 0;
        }
    }
}
