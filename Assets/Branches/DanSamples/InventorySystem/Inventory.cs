using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory")]
    public class Inventory : ScriptableObject
    {
        public List<ItemInstance> items = new();
    }
}
