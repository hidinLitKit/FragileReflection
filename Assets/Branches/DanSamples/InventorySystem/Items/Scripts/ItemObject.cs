using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public abstract class ItemObject : ScriptableObject
    {
        //Здесь содержатся также остальные, нужные UI
        public int ID;
        public bool unique;
        public string Name;
        [TextArea]
        public string Description;
        public Sprite image;
        public Item CreateItem()
        {
            Item newItem = new Item(this);
            return newItem;
        }
        public abstract void Use(); //Action that happens when you click use in Inventory
        public abstract void Examine(); //Action that happens when you click examine in Inventory

    }
    [System.Serializable]
    public class Item
    {
        //В этом классе поля важные системе инвентаря
        public string Name;
        public int ID;
        public bool unique;
        public Item(ItemObject item)
        {
            Name = item.Name;
            ID = item.ID;
            unique = item.unique;
        }
    }
}
