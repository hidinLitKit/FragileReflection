using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

namespace FragileReflection
{
    public interface IDamagable
    {
        void TakeDamage(float damage, int chance);
    }

    public class Damagable : MonoBehaviour, IDamagable
    {
        [Header("Коэффициент урона")]
        [SerializeField] private float k = 1;
        [SerializeField] private EnemyHealth enemyHealth;

        void IDamagable.TakeDamage(float damage, int chance)
        {
            if (enemyHealth != null)
            {
                int rand = Random.Range(0, 100);
                if(chance <= rand)
                    enemyHealth.Damage(damage * k);
            }
        }
    }
}
