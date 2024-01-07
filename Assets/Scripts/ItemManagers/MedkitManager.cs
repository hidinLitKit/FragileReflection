using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class MedkitManager : MonoBehaviour
    {
        public static MedkitManager instance { get; private set; }

        [SerializeField] private PlayerInfo _playerInfo;
        [SerializeField] private GameInventory _inventory;
        private InventoryObject _inventoryObject;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            instance = this;
            _inventoryObject = _inventory.inventory;
        }

        public void TryHeal(MedkitObject obj)
        {
            if(_inventoryObject.ItemAmmont(obj) > 0)
            {
                _playerInfo.HealAmmount(obj.healAmmount);
                _inventoryObject.RemoveItem(obj, 1);
            }

        }
    }
    
}
