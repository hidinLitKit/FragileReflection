using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FragileReflection
{
    public class Damagable : MonoBehaviour
    {
        [Range(0,200)]
        [SerializeField] private float maxHealth;

        private float health;

        private void Start()
        {
            health = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            
            if (health < 0)
                Die();
            else
                health -= damage;
        }

        private void Die()
        {
            var renderer = GetComponent<Renderer>();
            if (renderer == null)
            {
                Debug.Log($"No renderer but {this.name} died");
                return;
            }
            renderer.material.color = Color.red;
        }
    }
}
