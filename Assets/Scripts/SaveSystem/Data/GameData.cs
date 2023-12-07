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
        [Header("Inventory")]
        public SerializableDictionary<int, string> InventoryData;
        
        public GameData()
        {
            Debug.Log("New game started");
            //playerPosition = new Vector3(0, 0, 0);
            InventoryData = new SerializableDictionary<int, string>();
            StartValues();
            

        }
        private void StartValues()
        {
            //InventoryData.Add(0, "100 yen,0,True,0");
        }
    }
}
