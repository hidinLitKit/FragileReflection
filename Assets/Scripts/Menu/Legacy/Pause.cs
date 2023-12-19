using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
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
            GameEvents.onGamePause += EscPress;
        }

        private void OnDisable()
        {
            GameEvents.onGamePause -= EscPress;
        }
        
        
        public void OnContinue(bool s)
        {
            GameEvents.GamePause(s);
        }
        public void ExitGame()
        {
            Time.timeScale = 1;
        }

        public void EscPress(bool status)
        {
            _screenOptions.SetActive(false);
            _volumeOptions.SetActive(false);
            _buttonOptions.SetActive(false);
            _mainPause.SetActive(true);
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
