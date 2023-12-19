using System.Collections;
using UnityEngine;
using System;

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

        [Header("Точки остановки")]
        [SerializeField] private GameObject[] cameraPoints;

        private Transform playerCamera;

        [Header("Скорость передвижения камеры")]
        [SerializeField] private float moveSpeed = 30f;
        private int currentCameraIndex;

        public delegate void CameraMoveComplete();
        public static event CameraMoveComplete OnCameraMoveComplete;

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

            MainMenuScript.OnCameraMoveComplete += HandleCameraMoveComplete;
        }

        public void GotoOption()
        {
            MoveCameraToPoint(1);
            currentCameraIndex = 1;
        }

        public void GotoScreenOpt()
        {
            MoveCameraToPoint(2);
            currentCameraIndex = 2;
        }

        public void GotoVolumeOpt()
        {
            MoveCameraToPoint(2);
            currentCameraIndex = 3;
        }

        public void GotoMainMenu()
        {
            MoveCameraToPoint(0);
            currentCameraIndex = 0;
        }

        public void GotoOptionIn()
        {
            MoveCameraToPoint(1);
            currentCameraIndex = 1;
        }

        void MoveCameraToPoint(int newIndex)
        {
            StartCoroutine(MoveCameraSmooth(cameraPoints[newIndex].transform.position, () =>
            {
                OnCameraMoveComplete?.Invoke();
            }));
        }

        IEnumerator MoveCameraSmooth(Vector3 targetPosition, Action onComplete)
        {
            while (Vector3.Distance(playerCamera.position, targetPosition) > 0.01f)
            {
                playerCamera.position = Vector3.MoveTowards(playerCamera.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            onComplete?.Invoke();
        }

        void HandleCameraMoveComplete()
        {
            switch (currentCameraIndex)
            {
                case 0: //идем в главное
                    _optionsMenu.SetActive(false);
                    _mainMenu.SetActive(true);
                    break;
                case 1: //идем в настройки
                    _mainMenu.SetActive(false);
                    _screenOptions.SetActive(false);
                    _volumeOptions.SetActive(false);
                    _optionsMenu.SetActive(true);
                    break;
                case 2: //идем в настройки графики
                    _buttonOptions.SetActive(false);
                    _screenOptions.SetActive(true);
                    break;
                case 3: //идем в настройки музыки
                    _buttonOptions.SetActive(false);
                    _volumeOptions.SetActive(true);
                    break;
            }
        }
    }
}