using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FragileReflection
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePanel;

        private void OnEnable()
        {
            GameEvents.onGamePause += Paused;
        }

        private void OnDisable()
        {
            GameEvents.onGamePause -= Paused;
        }

        public void Paused(bool status)
        {
            _pausePanel.SetActive(status);

            if (status)
            {
                Time.timeScale = 0;
                Debug.Log("Pause start!");
            }
            else
            {
                Time.timeScale = 1;
                Debug.Log("Pause cancel!");
                InputManager.ToogleActionMaps("Player");
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameEvents.StaminaUIOpen();
            }
        }
    }
}
