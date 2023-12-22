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
        [Header("PuzzleData")]
        public SerializableDictionary<string, string> PuzzleData;        
        public GameData()
        {
            Debug.Log("New game started");
            playerPosition = new Vector3(-0.860000014f, 2.42000008f, -4.11999989f);
            maxHealth = 100;
            currentHealth = 100;
            InventoryData = new SerializableDictionary<int, string>();
            ActiveItmData = new SerializableDictionary<string, bool>();
            TriggersData = new SerializableDictionary<string, bool>();
            PuzzleData = new SerializableDictionary<string, string>();
            StartValues();
            

        }
        private void StartValues()
        {
            InventoryData.Add(0, "100 yen,0,True,0");
        }
    }
}
