using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    [System.Serializable]
    public class SerializableDictionary<Tkey, Tvalue> : Dictionary<Tkey, Tvalue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<Tkey> keys = new List<Tkey>();
        [SerializeField] private List<Tvalue> values = new List<Tvalue>();
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<Tkey, Tvalue> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }
        public void OnAfterDeserialize()
        {
            this.Clear();

            if (keys.Count != values.Count)
            {
                Debug.Log("Tried to deserialize SerializableDictionary, but keys Count " + keys.Count + "does not match values Count " + values.Count);
            }
            for (int i = 0; i < keys.Count; i++)
            {
                this.Add(keys[i], values[i]);
            }
        }

    }

}
