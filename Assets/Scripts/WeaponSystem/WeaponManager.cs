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
        public bool isWeaponEquiped = false;
        public Weapon currentWeapon = null;
        public PlayerAnimController playerAnimController;

        private int _unactiveLayer = 9;


        void Start()
        {
            if (instance == null) instance = this;
            else if (instance == this) Destroy(gameObject);
            SetWeapon();
        }
        private void OnEnable()
        {
            GameEvents.onWeaponChanged += SetWeapon;
        }
        private void OnDisable()
        {
            GameEvents.onWeaponChanged -= SetWeapon;
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
        private void SetWeapon()
        {
            foreach(Weapon weapon in weapons)
            {
                LayerChange(weapon.gameObject, _unactiveLayer);
                if (currentWeapon!=null && weapon.WeaponType == currentWeapon.WeaponType)
                {
                    LayerChange(weapon.gameObject, 0);
                }
            }
        }

        public void AttackPerform()
        {
            if (!currentWeapon.CanShoot()) return;
            currentWeapon.Fire();
            AttackAnim(); 
            
        }
        public void ReloadPerfom()
        {
            if (!currentWeapon.CanReload()) return;
            currentWeapon.Reload();
            ReloadAnim();
        }

        private void AttackAnim()
        {
            if(currentWeapon.GetType() == typeof(Pistol))
            {
                playerAnimController.PistolShoot();
            }
        }
        private void ReloadAnim()
        {
            if (currentWeapon.GetType() == typeof(Pistol))
            {
               playerAnimController.PistolReload();
            }
        }
        private void LayerChange(GameObject obj, int lay)
        {
            obj.layer = lay;
            foreach(Transform chld in obj.transform)
            {
                chld.gameObject.layer = lay;
            }
        }
    }
}
