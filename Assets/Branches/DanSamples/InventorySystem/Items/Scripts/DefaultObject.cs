using FragileReflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    [CreateAssetMenu(fileName = "New Default Object", menuName = "ScriptableObjects/Inventory System/Items/Default")]
    public class DefaultObject : ItemObject
    {
        private void Awake()
        {
            //что то
        }
        public override void Use()
        {
            //Action that happens when you click use in Inventory
        }
        public override void Examine()
        {
            //Action that happens when you click examine in Inventory
        }


    }
}
