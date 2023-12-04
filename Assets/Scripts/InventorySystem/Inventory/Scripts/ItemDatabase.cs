using FragileReflection;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace FragileReflection
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "ScriptableObjects/Inventory System/Database")]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
    {
        public List<ItemObject> Items;
        //public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

        public void OnAfterDeserialize()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].ID = i;
                //GetItem.Add(i, Items[i]);
            }
        }

        public void OnBeforeSerialize()
        {
            //GetItem = new Dictionary<int, ItemObject>();
        }
    }

}
