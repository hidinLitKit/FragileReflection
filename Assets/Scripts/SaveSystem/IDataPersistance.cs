using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public interface IDataPersistence
    {
        void LoadData(GameData data);
        void SaveData(GameData data);
    }

}
