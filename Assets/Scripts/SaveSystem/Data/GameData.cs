using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    [System.Serializable]
    public class GameData
    {
        [Header("Player")]
        public Vector3 playerPosition;
        public float maxHealth;
        public float currentHealth;
        [Header("Inventory")]
        public SerializableDictionary<int, string> InventoryData;
        [Header("Items on ground")]
        public SerializableDictionary<string, bool> ActiveItmData;
        [Header("Triggers")]
        public SerializableDictionary<string, bool> TriggersData;
        
        public GameData()
        {
            Debug.Log("New game started");
            //playerPosition = new Vector3(0, 0, 0);
            maxHealth = 100;
            currentHealth = 100;
            InventoryData = new SerializableDictionary<int, string>();
            ActiveItmData = new SerializableDictionary<string, bool>();
            TriggersData = new SerializableDictionary<string, bool>();
            StartValues();
            

        }
        private void StartValues()
        {
            InventoryData.Add(0, "100 yen,0,True,0");
        }
    }
}
