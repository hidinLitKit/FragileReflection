using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FragileReflection
{
    public class PlayerAimController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private CinemachineVirtualCamera _camera;

        private float zoomSpeed = 20f;
        private bool _zoomed = false;

        private InputActionMap _playerMap;
        private InputAction _moveAction;
        private InputAction _zoomAction;


        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Awake()
        {
            _playerMap = _inputActionAsset.FindActionMap("Player");
            _zoomAction = _playerMap.FindAction("Zoom");

        }

        private void OnEnable()
        {
            _playerMap.Enable();
            _zoomAction.performed += OnMouseRightClickPerformed;
            _zoomAction.canceled += OnMouseRightClickCanceled;
        }

        private void OnDisable()
        {
            _playerMap.Disable();
            _zoomAction.performed -= OnMouseRightClickPerformed;
            _zoomAction.canceled -= OnMouseRightClickCanceled;
        }

        private void OnMouseRightClickPerformed(InputAction.CallbackContext context)
        {
            _camera.m_Lens.FieldOfView -= zoomSpeed;
            _zoomed = false;
        }

        private void OnMouseRightClickCanceled(InputAction.CallbackContext context)
        {
            _camera.m_Lens.FieldOfView += zoomSpeed;
            _zoomed = true;
        }

        
    }
}
