using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class EnemyEventHandler : MonoBehaviour
    {
        [SerializeField] private EnemyController controller;
        public void AttackHit()
        {
            controller.AttackPlayer();
        }
    }
}
