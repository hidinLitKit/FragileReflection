using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace FragileReflection
{
    public class Pause : MonoBehaviour
    {
        [Header("Панель паузы")]
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _mainPause;

        [Header("Настройки")]
        [SerializeField] private GameObject _option;
        [SerializeField] private GameObject _buttonOptions;
        [SerializeField] private GameObject _screenOptions;
        [SerializeField] private GameObject _volumeOptions;
        //[SerializeField] private GameObject _controlOpt;

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

        public void ExitGame()
        {
            Time.timeScale = 1;
        }

        public void GotoMain()
        {
            SceneManager.LoadScene("Main Menu");
        }

        public void GotoScreenOpt()
        {
            _buttonOptions.SetActive(false);
            _screenOptions.SetActive(true);
        }

        public void GotoVolumeOpt()
        {
            _buttonOptions.SetActive(false);
            _volumeOptions.SetActive(true);
        }

        public void GotoOptionIn()
        {
            _screenOptions.SetActive(false);
            _volumeOptions.SetActive(false);
            _buttonOptions.SetActive(true);
        }

        public void GotoPause()
        {
            _buttonOptions.SetActive(false);
            _mainPause.SetActive(true);
        }

        public void GotoOption()
        {
            _mainPause.SetActive(false);
            _option.SetActive(true);
            _buttonOptions.SetActive(true);
        }
    }
}
