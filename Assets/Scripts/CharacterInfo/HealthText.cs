using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FragileReflection
{
    public class HealthText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        private PlayerInfo _playerParam;

        private void OnEnable()
        {
            GameEvents.onHealthImg += ShowUI;
            GameEvents.onHealthImg += ShowUI;
        }

        private void OnDisable()
        {
            GameEvents.onHealthImg -= ShowUI;
            GameEvents.onHealthImg -= HideUI;
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
            if (_playerParam != null)
            {
                _healthText.text = "Health: " + Mathf.RoundToInt(_playerParam.Health);
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
