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
        [SerializeField] private WeaponObject wpItem;
       
        public override string GetDescription()
        {
            return "взять оружие";
        }
        public override void Interact()
        {
            Debug.Log(wp);
            WeaponManager.AddWeapon(wp);
            Item itm = new Item(wpItem);
            GameInventory.instance.inventory.AddItem(itm, 1);
            Debug.Log("Click");
            //gameObject.SetActive(false);
        }
    }
}
