using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class ItemRequire : Interactable, IDataPersistence
    {
        [SerializeField] private KeyObject key;
        [SerializeField] private string _lockedMes;
        [SerializeField] private string _unlockedMes = "...";
        [SerializeField] private bool _doesDissapear = false;
        [SerializeField] private bool _isLocked = true;
        [SerializeField] private string _id;

        public delegate void OnInteract();
        public OnInteract InteractEvent;
        public KeyObject Key => key;
        public bool IsLocked => _isLocked;
        [ContextMenu("Generate id")]
        private void GenerateGuid()
        {
            _id = System.Guid.NewGuid().ToString();
        }
        private void OnValidate()
        {
            if (_id == string.Empty)
            {
                Debug.LogWarning("Object" + gameObject.name + "has not been initialized with save value");
            }
        }
        public void ChangeLockState()
        {
            _isLocked = !_isLocked;
            if (_doesDissapear) ObjectState.LayerChange(gameObject, ObjectState.UnactiveLayer);
            InteractEvent?.Invoke();
        }

        public override string GetDescription()
        {
            if (_isLocked) return _lockedMes;
            return _unlockedMes;
        }

        public override void Interact()
        {
            if (!_isLocked) return;
            if (GameInventory.instance.inventory.ContainsObject(key))
            {
                GameEvents.UseKey(key);
            }
        }

        public void LoadData(GameData data)
        {
            bool _isLck;
            if (!data.TriggersData.TryGetValue(_id, out _isLck)) return;
            _isLocked = _isLck;
        }

        public void SaveData(GameData data)
        {
            Debug.Log(gameObject + " is locked: " + _isLocked);
            if (data.TriggersData.ContainsKey(_id))
            {
                data.TriggersData.Remove(_id);
            }
            data.TriggersData.Add(_id, _isLocked);
            Debug.Log(gameObject + " saved, id and _isLocked " + _id + " " + _isLocked);
        }
    }
}

