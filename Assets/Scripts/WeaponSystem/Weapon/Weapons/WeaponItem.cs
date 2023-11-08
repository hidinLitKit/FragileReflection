using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using WeaponSystem;

namespace FragileReflection
{
    public class WeaponItem : Interactable
    {
        [SerializeField] private Weapon wp;
        public override string GetDescription()
        {
            return "взять оружие";
        }
        public override void Interact()
        {
            Debug.Log(wp);
            WeaponManager.AddWeapon(wp);
            Debug.Log("Click");
            //gameObject.SetActive(false);
        }
    }
}
