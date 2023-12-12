using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;
namespace FragileReflection
{
    public class Machete : Weapon
    {
        private Collider _attackCollider;
        private void Awake()
        {
            _attackCollider = GetComponent<Collider>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IDamagable enemyHealth))
            {
                enemyHealth.TakeDamage(WeaponManager.instance.currentWeapon.WeaponType.BodyDamage);
                _attackCollider.enabled = false;
            }
        }
        public override bool CanShoot()
        {
            return base._canShoot;
        }
        public override void Fire()
        {
            if (!CanShoot()) return;
            _canShoot = false;
            _attackCollider.enabled = true;
           StartCoroutine(RateCD(_weaponType.RateOfFire));
            
        }
        public override IEnumerator RateCD(float delay)
        {
            yield return new WaitForSeconds(delay/2);
            _attackCollider.enabled = false;
            yield return new WaitForSeconds(delay/2);
            _canShoot = true;
        }
        public override bool CanReload()
        {
            return false;
        }
        public override void Reload()
        {
            return;
        }
    }
}
