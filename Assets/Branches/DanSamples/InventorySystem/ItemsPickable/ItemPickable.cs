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
            throw new System.NotImplementedException();
        }

        public override void Interact()
        {

            throw new System.NotImplementedException();
        }

    }
}
