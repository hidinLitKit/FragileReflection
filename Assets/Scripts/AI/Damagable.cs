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
        //[Range(0, 200)]
        //[SerializeField] private float maxHealth;

        //[SerializeField] private float health;

        [Header("Коэффициент урона")]
        [SerializeField] private float k = 1;
        [SerializeField] private EnemyHealth enemyHealth;

        void IDamagable.TakeDamage(float damage)
        {
            //Debug.Log("Damage taken! " + damage);
            //health -= damage;
            if (enemyHealth != null)
            {
                enemyHealth.Damage(damage * k);
            }
        }

        //private void Die()
        //{
        //    if(gameObject.TryGetComponent(out EnemyController enemyController))
        //    {
        //        enemyController.Die();
        //    }
        //    Debug.Log($"{gameObject.name} died");
        //}

        //IEnumerator ResetStuggle()
        //{
        //    yield return new WaitForSeconds(0.4f);
        //    if (gameObject.TryGetComponent(out EnemyController enemyController))
        //    {
        //        enemyController.stuggled = false;
        //    }
            
        //}
    }
}
