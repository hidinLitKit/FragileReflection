using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public abstract class ItemObject : ScriptableObject
    {
        //����� ���������� ����� ���������, ������ UI
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
    }
    [System.Serializable]
    public class Item
    {
        //� ���� ������ ���� ������ ������� ���������
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
