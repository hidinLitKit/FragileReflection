using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class ItemPickable : Interactable
    {
        [SerializeField] private ItemObject item;

        public override string GetDescription()
        {
            return "Âחÿעü ןנוהלוע";
        }

        public override void Interact()
        {
            Item _item = new Item(item);
            GameInventory.instance.inventory.AddItem(_item, 1);
        }

    }
}
