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
        private readonly string _levelToLoad = "Main";
        public void NewGameButton()
        {
            StartCoroutine(NewGame());
        }
        public void ContinueGameButton()
        {
            Debug.Log("Game Continue");
            LoadLevel();
        }

        public void LoadLevel()
        {
            _mainMenu.SetActive(false);
            _loadingScreen.SetActive(true);

            StartCoroutine(LoadLevelASync());
        }

        IEnumerator LoadLevelASync()
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(_levelToLoad);

            while (!loadOperation.isDone)
            {
                float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
                _loadingSlider.value = progressValue;
                yield return null;
            }
        }
        IEnumerator NewGame()
        {
            Debug.Log("New game");
            DataPersistenceManager.instance.NewGame();
            yield return new WaitForSeconds(2f);
            DataPersistenceManager.instance.SaveGame();
            LoadLevel();
        }    
    }
}
