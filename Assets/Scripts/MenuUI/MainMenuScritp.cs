using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;
using UnityEngine.SceneManagement;

namespace FragileReflection
{
    public class ManeMenuScritp : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _optionsMenu;

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene("Main");
        }

        public void OptionOpen()
        {
            _mainMenu.SetActive(false);
            _optionsMenu.SetActive(true);
        }

        public void MainOpen()
        {
            _optionsMenu.SetActive(false);
            _mainMenu.SetActive(true);
        }

    }
}