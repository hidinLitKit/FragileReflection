using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FragileReflection
{
    public class HealthStatus : MonoBehaviour
    {
        private PlayerInfo _playerParam;

        private void OnEnable()
        {
            GameEvents.onStatusUI += UpdateUI;
        }

        private void Start()
        {
            _playerParam = FindObjectOfType<PlayerInfo>();

            if (_playerParam == null)
            {
                Debug.LogError("PlayerParam not found!");
            }
        }

        private void Update()
        {
            UpdateUI(true);
        }

        private void UpdateUI(bool status)
        {
            if (_playerParam != null)
            {
                if (_playerParam.Health >= 60)
                {
                    GameEvents.HealthChange("pulse_green_sprite", 30, "FINE", Color.green);
                }
                else if (_playerParam.Health >= 40)
                {
                    GameEvents.HealthChange("pulse_yellow_sprite", 60, "BAD", Color.yellow);
                }
                else
                {
                    GameEvents.HealthChange("pulse_red_sprite", 144, "DANGER", Color.red);
                }
            }
        }
    }
}
