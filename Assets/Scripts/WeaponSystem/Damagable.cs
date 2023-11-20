using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FragileReflection
{
    public interface IDamagable
    {
        void TakeDamage(float damage);
    }

    public class Damagable : MonoBehaviour, IDamagable
    {
        [Range(0, 200)]
        [SerializeField] private float maxHealth;

        private float health;
        void Start()
        {
            health = maxHealth;
        }

        void IDamagable.TakeDamage(float damage)
        {
            if (health <= 0)
                Die();
            else
                health -= damage;

            Debug.Log("Damage taken! " + damage);
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
