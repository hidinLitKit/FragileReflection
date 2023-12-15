using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class EnemyHealth : MonoBehaviour
    {
        [Range(0, 200)]
        [SerializeField] private float maxHealth;

        private float health;

        void Start()
        {
            health = maxHealth;
        }

        public void Damage(float damage, bool stuggle)
        {
            health -= damage;
            Debug.Log("Damage taken! " + damage);

            if (health <= 0)
                Die();
            else if (gameObject.TryGetComponent(out EnemyController enemyController))
            {
                enemyController.stuggled = stuggle;
                if (stuggle) StartCoroutine(ResetStuggle());
            }
        }

        private void Die()
        {
            if (gameObject.TryGetComponent(out EnemyController enemyController))
            {
                enemyController.Die();
            }
            Debug.Log($"{gameObject.name} died");
        }

        IEnumerator ResetStuggle()
        {
            yield return new WaitForSeconds(0.4f);
            if (gameObject.TryGetComponent(out EnemyController enemyController))
            {
                enemyController.stuggled = false;
                Debug.Log("Health! " + health);
            }

        }
    }
}
