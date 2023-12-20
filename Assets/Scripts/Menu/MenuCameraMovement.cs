using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class MenuCameraMovement : MonoBehaviour
    {
        [Header("Точки остановки")]
        [SerializeField] private GameObject[] _cameraPoints;

        private Transform _mainCamera;

        [Header("Скорость передвижения камеры")]
        [SerializeField] private float _moveSpeed = 30f;
        private CanvasGroup _canvasGroup;
        private Quaternion _originalRotation;

        void Start()
        {
            _mainCamera = Camera.main.transform;
            _canvasGroup = GetComponent<CanvasGroup>();
            _originalRotation = _mainCamera.rotation;
        }

        public void GotoTarget(int index)
        {
            MoveCameraToPoint(index);
        }

        void MoveCameraToPoint(int newIndex)
        {
            StartCoroutine(MoveCameraSmooth(_cameraPoints[newIndex].transform.position, newIndex == 2));
        }

        IEnumerator MoveCameraSmooth(Vector3 targetPosition, bool rotateOnPoint)
        {
            _canvasGroup.interactable = false;

            Quaternion targetRotation = rotateOnPoint ? Quaternion.Euler(0, -90, 0) : _originalRotation;

            float elapsedTime = 0f;
            float duration = _moveSpeed / 10f;

            while (elapsedTime < duration)
            {
                float t = EaseInOutQuint(elapsedTime / duration);
                _mainCamera.position = Vector3.Lerp(_mainCamera.position, targetPosition, t);
                _mainCamera.rotation = Quaternion.Slerp(_mainCamera.rotation, targetRotation, t);
                elapsedTime += Time.deltaTime;

                if (Vector3.Distance(_mainCamera.position, targetPosition) < 0.01f)
                {
                    break;
                }

                yield return null;
            }

            _mainCamera.SetPositionAndRotation(targetPosition, targetRotation);
            _canvasGroup.interactable = true;
        }

        float EaseInOutQuint(float t)
        {
            t /= 0.5f;
            if (t < 1) 
                return 0.5f * t * t * t * t * t;

            t -= 2;
            return 
                0.5f * (t * t * t * t * t + 2);
        }
    }
}
