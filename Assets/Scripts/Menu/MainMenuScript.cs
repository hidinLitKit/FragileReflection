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

        [Header("Точки остановки")]
        public GameObject[] cameraPoints;

        private Transform playerCamera;
        [Header("Скорость передвижения камеры")]
        public float moveSpeed = 5f;

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        void Start()
        {
            playerCamera = Camera.main.transform;
            MoveCameraToPoint(0);
        }

        public void GotoOption()
        {
            MoveCameraToPoint(1);
            _mainMenu.SetActive(false);
            _optionsMenu.SetActive(true);
        }

        public void GotoScreenOpt()
        {
            MoveCameraToPoint(2);
            _buttonOptions.SetActive(false);
            _screenOptions.SetActive(true);
        }

        public void GotoVolumeOpt()
        {
            MoveCameraToPoint(2);
            _buttonOptions.SetActive(false);
            _volumeOptions.SetActive(true);
        }
        
        public void GotoMainMenu()
        {
            MoveCameraToPoint(0);
            _optionsMenu.SetActive(false);
            _mainMenu.SetActive(true);
        }

        public void GotoOptionIn()
        {
            MoveCameraToPoint(1);
            _screenOptions.SetActive(false);
            _volumeOptions.SetActive(false);
            _buttonOptions.SetActive(true);
        }

        void MoveCameraToPoint(int newIndex)
        {
            StartCoroutine(MoveCameraSmooth(cameraPoints[newIndex].transform.position));
        }

        IEnumerator MoveCameraSmooth(Vector3 targetPosition)
        {
            while (Vector3.Distance(playerCamera.position, targetPosition) > 0.01f)
            {
                playerCamera.position = Vector3.MoveTowards(playerCamera.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}