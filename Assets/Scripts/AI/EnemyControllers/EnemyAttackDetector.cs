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
            if (other.tag != "Player") return;
            Debug.Log(other.name);
            IDamagable playerHealth = other.gameObject.GetComponent<IDamagable>();
            if (playerHealth != null)
            {
                hasAttacked = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IDamagable playerHealth = other.gameObject.GetComponent<IDamagable>();
            if (playerHealth != null)
            {
                hasAttacked = false;
            }
        }
    }
}
