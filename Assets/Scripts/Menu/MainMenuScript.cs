using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

        [Header("Игра сохранена!")]
        [SerializeField] private TMP_Text _text;
        private readonly float _fadeInTime = 1.0f;
        private readonly float _displayTime = 1.0f;
        private readonly float _fadeOutTime = 1.0f;
        
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

        public void OptionSave()
        {
            StartCoroutine(FadeInOut());
        }

        IEnumerator FadeInOut()
        {
            // Появление
            float elapsedTime = 0f;
            Color startColor = _text.color;
            Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

            while (elapsedTime < _fadeInTime)
            {
                _text.color = Color.Lerp(startColor, targetColor, elapsedTime / _fadeInTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(_displayTime);

            // Исчезновение
            elapsedTime = 0f;
            startColor = _text.color;
            targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

            while (elapsedTime < _fadeOutTime)
            {
                _text.color = Color.Lerp(startColor, targetColor, elapsedTime / _fadeOutTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _text.color = targetColor;
        }
    }
}