using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Linq;
//using static UnityEditor.Progress;
using System;

namespace FragileReflection
{
    public class ItemPickable : Interactable, IDataPersistence
    {
        
        [SerializeField] private ItemObject item;
        [SerializeField] private AudioClip _pickClip;
        [SerializeField] private int _amount;
        [Tooltip("Text that's being displayed when character sees object")]
        [SerializeField] private string _message;
        [SerializeField] private bool _isActive;
        [SerializeField] private string _id;

        private void Awake()
        {
            _isActive = true;
        }
        [ContextMenu("Generate id")]
        private void GenerateGuid()
        {
            _id = System.Guid.NewGuid().ToString();
        }
        private void OnValidate()
        {
            if(_id == string.Empty)
            {
                Debug.LogWarning("Object" + gameObject.name + "has not been initialized with save value");
            }
        }
        public override string GetDescription()
        {
            return _message;
        }
        public override void Interact()
        {
            Item _item = new Item(item);
            GameInventory.instance.inventory.AddItem(_item, _amount);
            _isActive = false;
            ObjectState.LayerChange(gameObject, ObjectState.UnactiveLayer);
            AudioEvents.instance.PlaySound(AudioEvents.instance.pickAudio, _pickClip);
        }
        public void LoadData(GameData data)
        {
            bool _isAct;
            if(!data.ActiveItmData.TryGetValue(_id, out _isAct)) return;
            _isActive = _isAct;
            if (!_isActive) ObjectState.LayerChange(gameObject, ObjectState.UnactiveLayer);        
        }

        public void SaveData(GameData data)
        {
            Debug.Log(gameObject + " is active: " + _isActive);
            if (data.ActiveItmData.ContainsKey(_id))
            {
                data.ActiveItmData.Remove(_id);
            }
            data.ActiveItmData.Add(_id, _isActive);
            Debug.Log(gameObject + " saved, id and _isActive " + _id + " " + _isActive);
        }
    }
}
