using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FragileReflection
{
    public class ASyncManager : MonoBehaviour
    {
        [Header("Menu Screens")]
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private GameObject _mainMenu;

        [Header("Slider")]
        [SerializeField] private Slider _loadingSlider;

        public void LoadLevelBtn(string levelToLoad)
        {
            _mainMenu.SetActive(false);
            _loadingScreen.SetActive(true);

            StartCoroutine(LoadLevelASync(levelToLoad));
        }

        IEnumerator LoadLevelASync(string levelToLoad)
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

            while (!loadOperation.isDone)
            {
                float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
                _loadingSlider.value = progressValue;
                yield return null;
            }
        }
    }
}
