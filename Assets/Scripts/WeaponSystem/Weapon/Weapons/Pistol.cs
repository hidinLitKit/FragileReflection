using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace FragileReflection
{
    public class Pistol : Weapon, IDataPersistence
    {
        public int ammoLeft = 0;
        [SerializeField] private WeaponRay weaponRay;
        [SerializeField] private WeaponObject _inventoryWeapon;
        [SerializeField] private string _id;
        [ContextMenu("Generate id")]
        private void GenerateGuid()
        {
            _id = System.Guid.NewGuid().ToString();
        }
        private void OnValidate()
        {
            if (_id == string.Empty)
            {
                Debug.LogWarning("Object" + gameObject.name + "has not been initialized with save value");
            }
        }
        private void Awake()
        {
            _bulletsLeft = WeaponType.Magazine;
        }
        public override void Fire()
        {
            if (!CanShoot()) { return; }
            weaponRay.TakeShot();//можно передать WeaponType для звуков
            _canShoot = false;
            _canReload = true;

            WeaponSound(_attackSound);
            
            _bulletsLeft--;
            Debug.Log("Bullets left: " + _bulletsLeft);
            GameEvents.Fire();
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
            WeaponSound(_reloadSound);
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
        
        public void LoadData(GameData data)
        {
            int _ammoLeft ;
            if (!data.WeaponData.TryGetValue(_id, out _ammoLeft)) return;
            _bulletsLeft = _ammoLeft;
        }

        public void SaveData(GameData data)
        {
            Debug.Log(gameObject + " has ammo: " + _bulletsLeft);
            if (data.WeaponData.ContainsKey(_id))
            {
                data.WeaponData.Remove(_id);
            }
            data.WeaponData.Add(_id, _bulletsLeft);
            Debug.Log($"{gameObject} +  saved, id and _bulletsLeft  {_id}  , {_bulletsLeft}");
        }
    }
}
