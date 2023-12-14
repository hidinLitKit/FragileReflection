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
        [SerializeField] private int _amount;
        [SerializeField] private string _message;
        [SerializeField] private string ID;
        [SerializeField] private bool isActive;
        [HideInInspector, SerializeField] private string _id;

        private bool _isAct;
        private int _unactiveLayer = 9;
        private void Awake()
        {
            isActive = true;
        }
        [ContextMenu("Generate id")]
        private void GenerateGuid()
        {
            _id = System.Guid.NewGuid().ToString();
            ID = _id;
        }
        private void OnValidate()
        {
            if(ID == string.Empty)
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
            isActive = false;
            LayerChange();
        }

        private void LayerChange()
        {
            gameObject.layer = _unactiveLayer;
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.layer = _unactiveLayer;
            }
        }
        public void LoadData(GameData data)
        {   
            if(!data.ActiveItmData.TryGetValue(_id, out _isAct)) return;
            isActive = _isAct;
            if (!isActive) LayerChange(); 
        }

        public void SaveData(GameData data)
        {
            Debug.Log(gameObject + " is active: " + isActive);
            if (data.ActiveItmData.ContainsKey(_id))
            {
                data.ActiveItmData.Remove(_id);
            }
            data.ActiveItmData.Add(_id, isActive);
            Debug.Log(gameObject + " saved, id and isActive " + _id + " " + isActive);
        }
    }
}
