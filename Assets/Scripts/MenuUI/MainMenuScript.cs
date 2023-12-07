using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;
using UnityEngine.SceneManagement;

namespace FragileReflection
{
    public class ManeMenuScritp : MonoBehaviour
    {
        [Header("Меню")]
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _optionsMenu;

        [Header("Загрузка")]
        [SerializeField] private GameObject _downloadBar;
        [SerializeField] private RectTransform _objectToMove;
        [SerializeField] private RectTransform _targetPositionBegin;
        [SerializeField] private RectTransform _targetPositionMid;
        [SerializeField] private RectTransform _targetPositionEnd;
        private float moveDuration = 2f;
        private float pauseDuration = 1f;

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
            StartCoroutine(LevelDownload());
        }

        private IEnumerator LevelDownload()
        {
            StartDownload();

            yield return new WaitForSeconds(5f);

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

        public void StartDownload()
        {
            _mainMenu.SetActive(false);
            _downloadBar.SetActive(true);
            StartCoroutine(DownloadFocus());
        }

        private IEnumerator DownloadFocus()
        {
            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                _objectToMove.position = Vector3.Lerp(_targetPositionBegin.position, _targetPositionMid.position, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(pauseDuration);

            elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                _objectToMove.position = Vector3.Lerp(_targetPositionMid.position, _targetPositionEnd.position, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

    }
}