using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace FragileReflection
{
    public class TriggerCamera : MonoBehaviour
    {
        private Transform playerTransform;
        private CinemachineVirtualCamera cam;
        private void Awake()
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
            cam = GetComponentInChildren<CinemachineVirtualCamera>();
            cam.LookAt = playerTransform;
            cam.Priority = 1;
            GetComponent<Collider>().isTrigger = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            cam.Priority = 99;
        }
        private void OnTriggerExit(Collider other)
        {
            cam.Priority = 1;
        }
    }
}
