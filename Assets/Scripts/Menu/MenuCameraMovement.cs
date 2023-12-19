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

        void Start()
        {
            _mainCamera = Camera.main.transform;
            MoveCameraToPoint(0);
        }

        public void GotoTarget(int index)
        {
            MoveCameraToPoint(index);
        }

        void MoveCameraToPoint(int newIndex)
        {
            StartCoroutine(MoveCameraSmooth(_cameraPoints[newIndex].transform.position));
        }

        IEnumerator MoveCameraSmooth(Vector3 targetPosition)
        {
            while (Vector3.Distance(_mainCamera.position, targetPosition) > 0.01f)
            {
                _mainCamera.position = Vector3.MoveTowards(_mainCamera.position, targetPosition, _moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
