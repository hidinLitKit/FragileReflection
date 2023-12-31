using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected ScriptableWeapon _weaponType; //��������� ��
        [SerializeField] protected AudioSource _weaponAudio;
        [SerializeField] protected AudioClip _equipSound;
        [SerializeField] protected AudioClip _attackSound;
        [SerializeField] protected AudioClip _reloadSound;
        [SerializeField] protected AudioClip _noAmmoSound;
        public ScriptableWeapon WeaponType { //������� ������ ������
            get
            {
                return _weaponType;
            } 
        }
        public AudioClip equipSound => _equipSound;
       
        protected bool _canShoot = true;
        protected int _bulletsLeft;
        protected bool _canReload = false;
        protected bool _isReloading = false;
        public abstract void Fire();
        public abstract void Reload();
        public abstract bool CanReload();
        public virtual bool CanShoot()
        {
            if(_bulletsLeft == 0)
            {
                Debug.Log("No ammo sound");
                return false;
            }
            return _canShoot;
        }
        public virtual IEnumerator RateCD(float delay)
        {   
            yield return new WaitForSeconds(delay);
            _canShoot = true;
        }
        public IEnumerator ReloadCD(float delay, int patrons)
        {
            Debug.Log("Reloading started");
            yield return new WaitForSeconds(delay);
            _isReloading = false;
            _canReload = false;
            _bulletsLeft += patrons;
            _canShoot = true;
            Debug.Log("Realoading finished");
        }

        protected void WeaponSound(AudioClip clip)
        {
            _weaponAudio.clip = clip;
            _weaponAudio.Play();
        }

    }
}
