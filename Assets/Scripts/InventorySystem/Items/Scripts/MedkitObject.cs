using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    [CreateAssetMenu(fileName = "New Medkit", menuName = "ScriptableObjects/Inventory System/Items/Medkit")]
    public class MedkitObject : ItemObject
    {
        public float healAmmount;
        public override void Examine()
        {
            
        }

        public override void Use()
        {
            MedkitManager.instance.TryHeal(this);
        }

        
    }
}
