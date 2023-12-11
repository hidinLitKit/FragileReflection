using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace FragileReflection
{
    public class Pistol : Weapon //Разделить огнестрел и неогнестрел
    {
        public int ammoLeft = 0;
        [SerializeField] private WeaponObject _inventoryWeapon;
        private void Awake()
        {
            _bulletsLeft = WeaponType.Magazine;
        }
        public override void Fire()
        {
            if (!CanShoot()) return;
            GameEvents.Fire();//можно передать WeaponType для звуков
            _canShoot = false;
            _canReload = true;
            
            _bulletsLeft--;
            Debug.Log("Bullets left: " + _bulletsLeft);
            
            StartCoroutine(RateCD(_weaponType.RateOfFire));
            if (_bulletsLeft == 0) _canShoot = false;


        }
        public override bool CanReload()
        {
            if (_isReloading) return false;
            if (_bulletsLeft == WeaponType.Magazine || !IsLeftAmmo()) { Debug.Log("No ammo or Full"); return false; }
            return true;
        }
        public override void Reload()
        {
            CanReload();
            StopAllCoroutines();
            _isReloading = true;
            _canReload = false;
            _canShoot = false;

            int needBullets = WeaponType.Magazine - _bulletsLeft;
            //тут какой-то баг возникает  - было _inventoryWeapon.AmmoLeft() >= WeaponType.Magazine
            if (_inventoryWeapon.AmmoLeft() >= needBullets)
            {
                Debug.Log("Loading " + needBullets);
                StartCoroutine(ReloadCD(_weaponType.RechargeSpeed, needBullets));
                _inventoryWeapon.AmmoSet(needBullets);
                
            }
            else
            {
                Debug.Log("Loading partial magazine");
                StartCoroutine(ReloadCD(_weaponType.RechargeSpeed, _inventoryWeapon.AmmoLeft()));
                _inventoryWeapon.AmmoSet(_inventoryWeapon.AmmoLeft());
            }
            GameEvents.WeaponReload();
        }

        public bool IsLeftAmmo()
        {
            Debug.Log(_inventoryWeapon.AmmoLeft());
            return _inventoryWeapon.AmmoLeft() > 0;
            
        }

    }
}
