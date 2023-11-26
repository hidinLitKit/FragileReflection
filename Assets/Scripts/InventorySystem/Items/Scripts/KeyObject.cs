using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    [CreateAssetMenu(fileName = "New Key", menuName = "ScriptableObjects/Inventory System/Items/Key")]
    public class KeyObject : ItemObject
    {
        public override void Examine()
        {

        }

        public override void Use()
        {
            GameEvents.UseKey(this);
        }

    }
}
