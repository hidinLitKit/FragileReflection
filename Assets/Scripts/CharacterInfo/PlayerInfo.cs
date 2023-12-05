using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FragileReflection
{
    public class PlayerInfo : MonoBehaviour, IDamagable 
    {
        [SerializeField] private float maxHealth;
        [Range(0, 100)][SerializeField] private float health;

        public float Health => health;
        public float MaxHealth => maxHealth;

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

            if (keyboard != null && keyboard.cKey.wasPressedThisFrame)
            {
                health = 100f;
                Debug.Log("Health is full!");
            }

            if (health < 100 && health > 0 && keyboard != null && keyboard.qKey.wasPressedThisFrame)
            {
                StartHealing();
            }

            if (health >= 60)
            {
                GameEvents.HealthChange("pulse_green_sprite", 30, "FINE", Color.green);
            }
            else if (health >= 40)
            {
                GameEvents.HealthChange("pulse_yellow_sprite", 60, "BAD", Color.yellow);
            }
            else
            {
                GameEvents.HealthChange("pulse_red_sprite", 144, "DANGER", Color.red);
            }
        }

        private void OnEnable()
        {
            GameEvents.onMaxHealthIncrease += IncreaseMaxHealth;
            GameEvents.onMedkitUse += HealAmmount;
        }

        private void OnDisable()
        {
            GameEvents.onMaxHealthIncrease -= IncreaseMaxHealth;
            GameEvents.onMedkitUse -= HealAmmount;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;

            if (health <= 0)
                Die();

            Debug.Log($"Player taken damage: {damage}! Health: {health}");
        }


        private void Die()
        {
            Debug.Log("Player died!");

            //if (_deathPanel != null)
               //_deathPanel.SetActive(true);
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
                Debug.Log($"Healing iteration {i + 1} and health = {health}");

                if (health >= 100)
                {
                    health = 100;
                    break;
                }
            }
        }

        public bool canHeal()
        {
            if(Health>=MaxHealth) 
                return false;

            return true;
        }

        public void HealAmmount(float hp)
        {
            if (canHeal())
            {
                Debug.Log("Healing: " + hp);

                health += hp;
                if (Health >= MaxHealth) 
                    health = MaxHealth;
            }

            Debug.Log(health);
        }

        public void IncreaseMaxHealth(float hp)
        {
            Debug.Log("Increasing hp: " + hp);

            maxHealth += hp;
            health = maxHealth;

            Debug.Log(health);
        }
    }
}
