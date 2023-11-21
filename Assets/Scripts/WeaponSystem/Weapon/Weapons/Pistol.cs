using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace FragileReflection
{
    public class Pistol : Weapon //Разделить огнестрел и неогнестрел
    {
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
        public override void Reload()
        {
            if (_bulletsLeft == WeaponType.Magazine) return;
            _isReloading = true;
            _canReload = false;
            _canShoot = false;
            //какой то GameEvents reload
            StartCoroutine(ReloadCD(_weaponType.RechargeSpeed));
            


        }

    }
}
