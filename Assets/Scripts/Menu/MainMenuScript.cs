using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.VersionControl.Asset;
using UnityEngine.SceneManagement;

namespace FragileReflection
{
    public class MainMenuScript : MonoBehaviour
    {
        [Header("Меню")]
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _optionsMenu;

        [Header("Настройки")]
        [SerializeField] private GameObject _buttonOptions;
        [SerializeField] private GameObject _screenOptions;
        [SerializeField] private GameObject _volumeOptions;
        //[SerializeField] private GameObject _controlOpt;
        
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void GotoOption()
        {
            _mainMenu.SetActive(false);
            _optionsMenu.SetActive(true);
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
        
        public void GotoMainMenu()
        {
            _optionsMenu.SetActive(false);
            _mainMenu.SetActive(true);
        }

        public void GotoOptionIn()
        {
            _screenOptions.SetActive(false);
            _volumeOptions.SetActive(false);
            _buttonOptions.SetActive(true);
        }
    }
}