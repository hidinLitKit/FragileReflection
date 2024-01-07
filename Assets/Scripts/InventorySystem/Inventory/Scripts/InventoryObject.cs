using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    [CreateAssetMenu(fileName ="Inventory", menuName ="ScriptableObjects/Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public Inventory Container;
        public ItemDatabaseObject database;
        public void AddItem(Item _item, int _amount)
        {
            //Checking if this type of item is exists in Inventory
            for(int i = 0; i < Container.Items.Count; i++)
            {
                if (Container.Items[i].ID == _item.ID)
                {
                    if (_item.unique)
                    {
                        Debug.LogWarning("Attempt to add add unique item to inventory, but there already exists one");
                        return; 
                    }
                    Container.Items[i].AddAmount(_amount);
                    return;
                }
            }
            //this is new Item
            SetSlot(_item, _amount);
        }
        public void SetSlot(Item _item, int _amount)
        {
            InventorySlot newItem = new InventorySlot(_item.ID, _item, _amount, _item.unique);
            Container.Items.Add(newItem);
        }
        public void RemoveItem(int id)
        {
            for (int i = 0; i < Container.Items.Count; i++)
            {
                if (Container.Items[i].item.ID == id)
                {
                    Container.Items.Remove(Container.Items[i]);
                    return;
                }
            }
        }
        public void RemoveItem(int id, int ammount)
        {
            for (int i = 0; i < Container.Items.Count; i++)
            {
                if (Container.Items[i].item.ID == id)
                {
                    Container.Items[i].amount -= ammount;
                    if (Container.Items[i].amount <= 0) Debug.LogWarning($"Ammount of {Container.Items[i]} is lower than 0");
                    return;
                }
            }
        }
        public void RemoveItem(ItemObject item, int ammount)
        {
            for (int i = 0; i < Container.Items.Count; i++)
            {
                if (Container.Items[i].item.ID == item.ID)
                {
                    Container.Items[i].amount -= ammount;
                    if (Container.Items[i].amount <= 0) Debug.LogWarning($"Ammount of {Container.Items[i]} is lower than 0");
                    return;
                }
            }
        }
        public bool ContainsObject(ItemObject itm)
        {
            for (int i = 0; i < Container.Items.Count; i++)
            {
                if (Container.Items[i].ID == itm.ID)
                {
                   Debug.Log("Inventory contains " + itm.Name);
                    return true;
                }
            }
            return false;
        }
        public int ItemAmmont(ItemObject itm)
        { 
            for (int i = 0; i < Container.Items.Count; i++)
                {
                    if (Container.Items[i].item.ID == itm.ID)
                    {
                        return Container.Items[i].amount;
                    }
                }
            Debug.LogWarning("Item doesn't exist that is strange");
            return -1;
        }

    }
    [System.Serializable]
    public class Inventory
    {
        public List<InventorySlot> Items = new List<InventorySlot> ();
        //public InventorySlot[] Items = new InventorySlot[36];
    }
    [System.Serializable]
    public class InventorySlot
    {
        public int ID = -1; //ID ��� ����������� � Item ����� �� ����� �������� amount
        public Item item;
        public int amount;
        private bool unique;
        public InventorySlot()
        {
            ID = -1;
            item = null;
            amount = 0;
            unique = false;
        }
        public InventorySlot(int ID, Item item, int amount, bool unique)
        {
            this.ID = ID;
            this.item = item;
            this.amount = amount;
            this.unique = unique;
        }
        /* public void UpdateSlot(int _id, Item _item, int _amount, bool _unique)
        {
            ID = _id;
            item = _item;
            amount = _amount;
            unique = _unique;
        }
        */
        public void AddAmount(int value)
        {
            if(!unique) amount += value;
        }
    }
}
