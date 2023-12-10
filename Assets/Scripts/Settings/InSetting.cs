using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FragileReflection
{
    public class InSetting : MonoBehaviour
    {
        [SerializeField] private GameObject _mainOption;
        [SerializeField] private GameObject _screenOption;
        [SerializeField] private GameObject _volumeOption;

        public void GotoOption()
        {
            _screenOption.SetActive(false);
            _volumeOption.SetActive(false);
            _mainOption.SetActive(true);
        }

        public void GotoScreenOption()
        {
            _mainOption.SetActive(false);
            _screenOption.SetActive(true);
        }

        public void GotoMainMenu()
        {
            SceneManager.LoadScene("Main menu");
        }

        public void GotoVolumeOption() 
        {
            _mainOption.SetActive(false);
            _volumeOption.SetActive(true);
        }
    }
}
