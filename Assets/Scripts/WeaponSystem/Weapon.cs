using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected ScriptableWeapon _weaponType; //��������� ��
        public ScriptableWeapon WeaponType { //������� ������ ������
            get
            {
                return _weaponType;
            } 
        }
        protected bool _canShoot = true;
        protected int _bulletsLeft;
        protected bool _canReload = false;
        protected bool _isReloading = false;
        public abstract void Fire();
        public abstract void Reload();
        public bool CanShoot()
        {
            if(_bulletsLeft == 0)
            {
                Debug.Log("No ammo sound");
                return false;
            }
            return _canShoot;
        }
        public IEnumerator RateCD(float delay)
        {
            
            yield return new WaitForSeconds(delay);
            _canShoot = true;
        }
        public IEnumerator ReloadCD(float delay)
        {
            Debug.Log("Reloading started");
            yield return new WaitForSeconds(delay);
            _isReloading = false;
            _canReload = false;
            _bulletsLeft = WeaponType.Magazine;
            _canShoot = true;
            Debug.Log("Realoading finished");
        }



    }
}