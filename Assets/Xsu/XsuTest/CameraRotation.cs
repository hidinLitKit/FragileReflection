using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XsuTest
{
    public class CameraRotation : MonoBehaviour
    {
        public float cameraSpeed;

        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private CinemachineVirtualCamera _camera;

        private InputActionMap _map;
        private InputAction _moveCamera;
        private InputAction _hold;

        private Vector2 startPosOnScreen;
        private bool _pressed;
        private Vector2 prevcam = Vector2.zero;

        private CinemachineOrbitalTransposer _transposer;

        private void Awake()
        {
            _map = _inputActionAsset.FindActionMap("Player");

            _moveCamera = _map.FindAction("CameraRotation");
            _hold = _map.FindAction("HoldingCam");

            _transposer = _camera.GetCinemachineComponent<CinemachineOrbitalTransposer>();

            if(!_transposer)
            {
                throw new System.Exception($"Not Orbital transposer on camera {_camera.name}");
            }
        }
        private void OnEnable()
        {
            _map.Enable();
        }

        private void OnDisable()
        {
            _map.Disable();
        }

        private void Update()
        {
            if(_hold.ReadValue<float>() > 0.1f)
            {
                if(prevcam == Vector2.zero)
                     prevcam = _moveCamera.ReadValue<Vector2>();

                Vector2 cam = _moveCamera.ReadValue<Vector2>();

                if (!_pressed)
                {
                    _pressed = true;
                    startPosOnScreen = cam;
                }

                Vector2 diff = (startPosOnScreen - cam).normalized;

                if(Mathf.Abs(cam.x-prevcam.x) > 0.01f)
                {
                    float newVal = _transposer.m_Heading.m_Bias + diff.x * cameraSpeed;

                    newVal = newVal < -180 ? 180 : newVal;
                    newVal = newVal > 180 ? -180 : newVal;

                    if (startPosOnScreen.y > Screen.height * 0.2f)
                        _transposer.m_Heading.m_Bias = newVal;
                }
                

                prevcam = cam;
            }
            else
            {
                _pressed = false;
                prevcam = Vector2.zero;
            }
            
        }
    }
}
