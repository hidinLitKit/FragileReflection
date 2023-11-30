using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FragileReflection
{
    public class HealthStatus : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        private PlayerInfo _playerParam;

        private void OnEnable()
        {
            GameEvents.onStatusUI += ShowUI;
            GameEvents.onStatusUI += UpdateUI;
        }

        private void OnDisable()
        {
            GameEvents.onStatusUI -= ShowUI;
            GameEvents.onStatusUI -= HideUI;
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
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_playerParam != null)
            {
                if (_playerParam.Health >= 60)
                {
                    _healthText.color = Color.green;
                    _healthText.text = "FINE";

                    GameEvents.HealthChange("pulse_green_sprite", 30);
                }
                else if (_playerParam.Health >= 40)
                {
                    _healthText.color = Color.yellow;
                    _healthText.text = "BAD";

                    GameEvents.HealthChange("pulse_yellow_sprite", 60);
                }
                else
                {
                    _healthText.color = Color.red;
                    _healthText.text = "DANGER";

                    GameEvents.HealthChange("pulse_red_sprite", 144);
                }
            }
        }

        private void ShowUI()
        {
            _healthText.gameObject.SetActive(true);
        }

        private void HideUI()
        {
            _healthText.gameObject.SetActive(false);
        }
    }
}
