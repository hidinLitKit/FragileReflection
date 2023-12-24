using FragileReflection;
using System.Collections;
using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
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
                ObjectState.LayerChange(weapon.gameObject, ObjectState.UnactiveLayer);
                if (currentWeapon!=null && weapon.WeaponType == currentWeapon.WeaponType)
                {
                    ObjectState.LayerChange(weapon.gameObject, ObjectState.DefaultLayer);
                    AudioEvents.instance.PlaySound(AudioEvents.instance.weaponEquipAudio, weapon.equipSound);
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
            if(currentWeapon.GetType() == typeof(Pistol)) playerAnimController.PistolShoot();
            else if (currentWeapon.GetType() == typeof(Machete)) playerAnimController.MeleeAttack();
        }
        private void ReloadAnim()
        {
            if (currentWeapon.GetType() == typeof(Pistol)) playerAnimController.PistolReload();
           
        }
    }
}
