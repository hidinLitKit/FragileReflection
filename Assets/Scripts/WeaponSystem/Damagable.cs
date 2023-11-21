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

        [SerializeField] private float health;

        void Start()
        {
            health = maxHealth;
        }

        void IDamagable.TakeDamage(float damage)
        {
            Debug.Log("Damage taken! " + damage);
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
            else if (gameObject.TryGetComponent<EnemyController>(out EnemyController enemyController))
            {
                enemyController.stuggled = true;
                StartCoroutine("ResetStuggle");
            }

        }

        private void Die()
        {
            if(gameObject.TryGetComponent<EnemyController>(out EnemyController enemyController))
            {
                enemyController.Die();
            }
            Debug.Log($"{gameObject.name} died");
        }

        IEnumerator ResetStuggle()
        {
            yield return new WaitForSeconds(0.4f);
            if (gameObject.TryGetComponent<EnemyController>(out EnemyController enemyController))
            {
                enemyController.stuggled = false;
            }
            
        }
    }
}
