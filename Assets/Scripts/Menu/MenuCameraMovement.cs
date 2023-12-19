using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class MenuCameraMovement : MonoBehaviour
    {
        [Header("Точки остановки")]
        [SerializeField] private GameObject[] cameraPoints;

        private Transform playerCamera;

        [Header("Скорость передвижения камеры")]
        [SerializeField] private float moveSpeed = 30f;

        void Start()
        {
            playerCamera = Camera.main.transform;
            MoveCameraToPoint(0);
        }

        public void GotoTarget(int index)
        {
            MoveCameraToPoint(index);
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
