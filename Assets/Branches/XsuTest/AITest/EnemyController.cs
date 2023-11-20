using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dan
{
    public class EnemyController : MonoBehaviour
    {
        public Transform[] patrolEndpoints;

        public void Attack()
        {
            Debug.Log("Attacking");
        }
    }
}
