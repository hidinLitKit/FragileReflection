using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FragileReflection
{
    public class PlayerParam : MonoBehaviour
    {
        [Range(0, 100)][SerializeField] private float health;

        public float Health => health;

        [SerializeField] private GameObject _deathPanel;

        private Coroutine _healingCoroutine;
        [SerializeField] private float _heal = 2f;

        public enum HealingPower
        {
            Low,
            Medium,
            High
        }

        [SerializeField]
        [Header("Power Healing")]
        private HealingPower _powerHeal = HealingPower.Low;

        private void Update()
        {
            var keyboard = Keyboard.current;

            if (health > 0 && keyboard != null && keyboard.yKey.wasPressedThisFrame)
            {
                TakeDamage(5f);
            }

            if (keyboard != null && keyboard.tabKey.wasPressedThisFrame)
            {
                _deathPanel.SetActive(false);
                health = 100f;
            }

            if (health < 100 && health > 0 && keyboard != null && keyboard.qKey.wasPressedThisFrame)
            {
                StartHealing();
            }
        }

        public void TakeDamage(float damage)
        {

            health -= damage;

            if (health <= 0)
                Die();

            Debug.Log($"Damage talen: {damage}! Health: {health}");
        }

        private void Die()
        {
            Debug.Log("Player died!");

            if (_deathPanel != null)
                _deathPanel.SetActive(true);
        }

        private void StartHealing()
        {
            if (_healingCoroutine != null)
            {
                StopCoroutine(_healingCoroutine);
            }

            float healingRate = GetHealingRate(_powerHeal);
            _healingCoroutine = StartCoroutine(HealOverTime(10, healingRate));
        }

        private float GetHealingRate(HealingPower power)
        {
            return power switch
            {
                HealingPower.Low => 2f,
                HealingPower.Medium => 1.5f,
                HealingPower.High => 1f,
                _ => 1,
            };
        }

        private IEnumerator HealOverTime(int iterations, float healingRate)
        {
            for (int i = 0; i < iterations; i++)
            {
                yield return new WaitForSeconds(healingRate);
                health += _heal;
                Debug.Log($"Healing iteration {i + 1}");

                if (health >= 100)
                {
                    health = 100;
                    break;
                }
            }
        }
    }
}
