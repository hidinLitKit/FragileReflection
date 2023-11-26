using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class GameInventory : MonoBehaviour
    {
        [HideInInspector] public static GameInventory instance = null;
        public InventoryObject inventory;
        private void OnEnable()
        {
            GameEvents.onSuccesUse += ManageItem;
        }
        private void OnDisable()
        {
            GameEvents.onSuccesUse -= ManageItem;
        }
        void Start()
        {
            if (instance == null) instance = this;
            else if (instance == this) Destroy(gameObject);
            //DontDestroyOnLoad(gameObject);
            //»нициализируем менеджер (если будет нужен);
        }
        private void ManageItem(int id)
        {
            //возможный memoryleak (мы создаем копию item, чтобы удалить из инвентар€ оригинал item, сравнива€ тип копии и оригинала)
            inventory.RemoveItem(id);
        }

    }
}
