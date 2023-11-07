using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Dan
{
    public class Damagable : MonoBehaviour
    {
        [Range(0,200)]
        [SerializeField] private int maxHealth;

        private int health;

        private void Start()
        {
            health = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health < 0)
                Die();
        }

        private void Die()
        {
            var renderer = GetComponent<Renderer>();
            renderer.material.color = Color.red;
        }
    }
}
