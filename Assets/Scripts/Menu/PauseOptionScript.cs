using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.VersionControl.Asset;
using UnityEngine.SceneManagement;

namespace FragileReflection
{
    public class PauseOptionScript : MonoBehaviour
    {
        [Header("Настройки")]
        [SerializeField] private GameObject _buttonOptions;
        [SerializeField] private GameObject _screenOptions;
        [SerializeField] private GameObject _volumeOptions;
        //[SerializeField] private GameObject _controlOpt;

        [SerializeField] private GameObject _mainPause;

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
    }
}