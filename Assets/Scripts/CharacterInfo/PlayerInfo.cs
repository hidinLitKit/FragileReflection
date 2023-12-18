using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FragileReflection
{
    public class PlayerInfo : MonoBehaviour, IDamagable, IDataPersistence
    {
        [Header("Health")]
        [SerializeField] private float maxHealth;
        [Range(0, 100)][SerializeField] private float health;

        public float Health => health;
        public float MaxHealth => maxHealth;
        private bool isDead = false;
        private Coroutine _healingCoroutine;

        [Header("Power Healing")]
        [SerializeField] private float _heal = 2f;

        public enum HealingPower
        {
            Low,
            Medium,
            High
        }

        [SerializeField] private HealingPower _powerHeal = HealingPower.Low;

        private void Update()
        {
            var keyboard = Keyboard.current;

            if (health > 0 && keyboard != null && keyboard.yKey.wasPressedThisFrame)
            {
                TakeDamage(50f, 100);
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
           
            float healthPercentage = (health / maxHealth) * 100f;

            if (healthPercentage >= 66f)
            {
                GameEvents.HealthChange("pulse_green_sprite", 30, "FINE", Color.green);
            }
            else if (healthPercentage >= 33f)
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

        public void TakeDamage(float damage, int chance)
        {
            if (isDead) return;

            health -= damage;

            if (health <= 0)
                Die();

            Debug.Log($"Player taken damage: {damage}! Health: {health}");
        }


        private void Die()
        {
            Debug.Log("Player died!");
            isDead = true;
            States.instance.Push<DeathState>();
            GetComponent<PlayerAnimController>().Death();
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

        public void LoadData(GameData data)
        {
            this.gameObject.transform.position = data.playerPosition;
            health = data.currentHealth;
            maxHealth = data.maxHealth;
        }

        public void SaveData(GameData data)
        {
            data.playerPosition = this.gameObject.transform.position;
            data.currentHealth = health;
            data.maxHealth = maxHealth;

        }
    }
}