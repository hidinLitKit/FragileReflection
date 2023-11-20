using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class GameInventory : MonoBehaviour
    {
        [HideInInspector] public static GameInventory instance = null;
        public InventoryObject inventory;
        void Start()
        {
            if (instance == null) instance = this;
            else if (instance == this) Destroy(gameObject);
            //DontDestroyOnLoad(gameObject);
            //Инициализируем менеджер (если будет нужен);
        }
    }
}
