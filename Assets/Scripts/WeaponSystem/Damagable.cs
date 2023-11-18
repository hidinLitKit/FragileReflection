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
        EnemyController enemyController;

        void Start()
        {
            health = maxHealth;
        }

        void IDamagable.TakeDamage(float damage)
        {
            if (health - damage <= 0)
                return;

            if(enemyController != null)
            {
                enemyController.stuggled = true;
                StartCoroutine("ResetStuggle");
            }
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
                
            Debug.Log("Damage taken! " + damage);

        }

        private void Die()
        {
            if(enemyController != null)
            {
                enemyController.Die();
            }
            Debug.Log($"{gameObject.name} died");
            //var renderer = GetComponent<Renderer>();
            //if (renderer == null)
            //{
            //    Debug.Log($"No renderer but {gameObject.name} died");
            //    return;
            //}
            //renderer.material.color = Color.red;
        }

        IEnumerator ResetStuggle()
        {
            yield return new WaitForSeconds(2f);
            enemyController.stuggled = true;
        }
    }
}
