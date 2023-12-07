using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class PlayerData : MonoBehaviour, IDataPersistence
    {
        public void LoadData(GameData data)
        {
            this.gameObject.transform.position = data.playerPosition;
        }
        public void SaveData(GameData data)
        {
            data.playerPosition = this.gameObject.transform.position;
        }
    }
}
