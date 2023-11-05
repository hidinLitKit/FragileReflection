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
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private Transform _transformPlayer;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotateSpeed = 40f;

        private float zoomSpeed = 20f;
        private bool _zoomed = false;

        private InputActionMap _playerMap;
        private InputAction _moveAction;
        private InputAction _zoomAction;

        private InputAction _mouseLook;
        private Vector2 _rotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Awake()
        {
            _playerMap = _inputActionAsset.FindActionMap("Player");
            _moveAction = _playerMap.FindAction("Move");
            _zoomAction = _playerMap.FindAction("Zoom");
            _mouseLook = _playerMap.FindAction("Look");

            _rotation = Vector2.zero;
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

        private void Update()
        {
            var move = _moveAction.ReadValue<Vector2>();
            if (move != Vector2.zero)
            {
                var dir = new Vector3(move.x, 0, move.y);
                _characterController.SimpleMove(dir * moveSpeed);
            }

            var look = _mouseLook.ReadValue<Vector2>();
            Look(look);
        }

        private void Look(Vector2 rotate)
        {
            //��� �������������� ���������� �������� ������
            if (rotate.sqrMagnitude < 0.01)
                return;

            var scaledRotateSpeed = rotateSpeed * Time.deltaTime;
            _rotation.y += rotate.x * scaledRotateSpeed;
            if (!_zoomed)
            {
                _rotation.x = Mathf.Clamp(_rotation.x - rotate.y * scaledRotateSpeed, -30, 10);
                _transformPlayer.localEulerAngles = _rotation;
            }
            else
            {
                _rotation.x = Mathf.Clamp(_rotation.x - rotate.y * scaledRotateSpeed, -20, 10);
                _transformPlayer.localEulerAngles = _rotation;
            }
        }
    }
}