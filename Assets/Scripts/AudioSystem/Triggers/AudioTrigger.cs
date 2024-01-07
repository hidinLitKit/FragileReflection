using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public abstract class AudioTrigger : MonoBehaviour, IDataPersistence
    {
        [SerializeField] protected string _id;
        [SerializeField ] protected bool _doesDeactivate = true;
        [ContextMenu("Generate id")]
        protected void GenerateGuid()
        {
            _id = System.Guid.NewGuid().ToString();
        }
        protected void OnValidate()
        {
            if (_id == string.Empty)
            {
                Debug.LogWarning("Object" + gameObject.name + "has not been initialized with save value");
            }
        }
        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player") return;
            if(_doesDeactivate) GetComponent<Collider>().enabled = false;
            //GetComponent<AudioSource>().Play();
        }

        public void LoadData(GameData data)
        {
            bool _isColAct;
            if (!data.TriggersData.TryGetValue(_id, out _isColAct)) return;
            GetComponent<Collider>().enabled = _isColAct;
        }

        public void SaveData(GameData data)
        {
            bool _isColAct = GetComponent<Collider>().enabled;
            Debug.Log(gameObject + " collider active: " + _isColAct);
            if (data.TriggersData.ContainsKey(_id))
            {
                data.TriggersData.Remove(_id);
            }
            data.TriggersData.Add(_id, _isColAct);
            Debug.Log(gameObject + " saved, id and _isLocked " + _id + " " + _isColAct);
        }
    }
}
