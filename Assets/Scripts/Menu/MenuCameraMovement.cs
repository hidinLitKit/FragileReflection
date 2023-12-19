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
            MoveCameraToPoint(0);
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

            while (Vector3.Distance(_mainCamera.position, targetPosition) > 0.01f)
            {
                _mainCamera.position = Vector3.MoveTowards(_mainCamera.position, targetPosition, _moveSpeed * Time.deltaTime);
                _mainCamera.rotation = Quaternion.RotateTowards(_mainCamera.rotation, targetRotation, 90 * Time.deltaTime);
                yield return null;
            }

            _mainCamera.rotation = targetRotation;
            _canvasGroup.interactable = true;
        }
    }
}
