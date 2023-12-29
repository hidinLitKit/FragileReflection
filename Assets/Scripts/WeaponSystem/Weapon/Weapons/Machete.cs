using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;
namespace FragileReflection
{
    public class Machete : Weapon
    {
        [SerializeField] private List<AudioClip> _attackRnd; 
        private Collider _attackCollider;
        private void Awake()
        {
            _attackCollider = GetComponent<Collider>();
            _attackCollider.enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IDamagable enemyHealth))
            {
                if (gameObject.layer == 9) return; // unactive layer
                enemyHealth.TakeDamage(WeaponManager.instance.currentWeapon.WeaponType.BodyDamage, WeaponManager.instance.currentWeapon.WeaponType.chance);
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
            WeaponSoundRandom(_attackRnd);
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
