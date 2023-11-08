using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class PlayerParam : MonoBehaviour
    {
        [Range(0, 100)][SerializeField] private float health;

        public void TakeDamage(float damage)
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
