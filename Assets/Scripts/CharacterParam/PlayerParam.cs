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

        [SerializeField] private GameObject deathPanel;

        private void Update()
        {
            var keyboard = Keyboard.current;

            if (health > 0 && keyboard != null && keyboard.yKey.wasPressedThisFrame)
            {
                TakeDamage(5f);
            }

            if (keyboard != null && keyboard.tabKey.wasPressedThisFrame)
            {
                deathPanel.SetActive(false);
                health = 100f;
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

            if (deathPanel != null)
                deathPanel.SetActive(true);
        }


    }
}
