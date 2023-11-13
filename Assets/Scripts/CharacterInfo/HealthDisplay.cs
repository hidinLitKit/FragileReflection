using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FragileReflection
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;
        private PlayerInfo playerParam;

        private void Start()
        {
            playerParam = GetComponent<PlayerInfo>();

            if (healthText == null)
            {
                Debug.LogError("HealthText component not assigned!");
                return;
            }

            UpdateHealthText();
        }

        private void Update()
        {
            UpdateHealthText();
        }

        private void UpdateHealthText()
        {
            if (playerParam == null)
            {
                Debug.LogError("PlayerParam component not found!");
                return;
            }

            healthText.text = "Health: " + Mathf.RoundToInt(playerParam.Health);
        }
    }
}
