using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using WeaponSystem;

namespace FragileReflection
{
    public class EnemyAttackDetector : MonoBehaviour
    {
        public bool hasAttacked;

        private void OnTriggerEnter(Collider other)
        {
            IDamagable enemyHealth = other.gameObject.GetComponent<IDamagable>();
            if (enemyHealth != null)
            {
                hasAttacked = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IDamagable enemyHealth = other.gameObject.GetComponent<IDamagable>();
            if (enemyHealth != null)
            {
                hasAttacked = false;
            }
        }
    }
}
