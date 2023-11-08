using Cinemachine;
using FragileReflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dan
{
    public class CameraShaking : MonoBehaviour
    {
        [SerializeField] private CinemachineImpulseSource _impulseSource;

        private void OnEnable()
        {
            GameEvents.onFire += ShakeCamera;
        }
        private void OnDisable()
        {
            GameEvents.onFire -= ShakeCamera;
        }


        private void ShakeCamera()
        {
            _impulseSource.GenerateImpulse(Camera.main.transform.forward);
        }
    }
}
