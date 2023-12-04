using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class ItemPickable : Interactable
    {
        [SerializeField] private ItemObject item;
        [SerializeField] private int _amount;
        [SerializeField] private string _message;
        private int _unactiveLayer = 9;

        public override string GetDescription()
        {
            return _message;
        }

        public override void Interact()
        {
            Item _item = new Item(item);
            GameInventory.instance.inventory.AddItem(_item, _amount);
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
    }
}
