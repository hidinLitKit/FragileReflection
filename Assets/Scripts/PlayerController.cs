using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FragileReflection
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private Transform _transformPlayer;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotateSpeed = 40f;

        private float zoomSpeed = 20f;

        private InputActionMap _playerMap;
        private InputAction _moveAction;
        private InputAction _zoomAction;

        private InputAction _mouseLook;
        private Vector2 _rotation;

        private void Awake()
        {
            _playerMap = _inputActionAsset.FindActionMap("Player");
            _moveAction = _playerMap.FindAction("Move");

            _zoomAction = _playerMap.FindAction("Zoom");
            _zoomAction.performed += OnMouseRightClickPerformed;
            _zoomAction.canceled += OnMouseRightClickCanceled;

            _mouseLook = _playerMap.FindAction("Look");
            _rotation = Vector2.zero;
        }

        private void OnEnable()
        {
            _playerMap.Enable();
        }

        private void OnDisable()
        {
            _playerMap.Disable();
        }

        private void OnMouseRightClickPerformed(InputAction.CallbackContext context)
        {
            _camera.m_Lens.FieldOfView -= zoomSpeed;
        }

        private void OnMouseRightClickCanceled(InputAction.CallbackContext context)
        {
            _camera.m_Lens.FieldOfView += zoomSpeed;
        }

        private void Update()
        {
            var move = _moveAction.ReadValue<Vector2>();
            if (move != Vector2.zero)
            {
                Console.WriteLine(move);
                var dir = new Vector3(move.x, 0, move.y);
                _characterController.SimpleMove(dir * moveSpeed);
            }

            var look = _mouseLook.ReadValue<Vector2>();
            Look(look);
        }

        private void Look(Vector2 rotate)
        {
            //для предотвращения случайного поворота камеры
            if (rotate.sqrMagnitude < 0.01)
                return;

            var scaledRotateSpeed = rotateSpeed * Time.deltaTime;
            _rotation.y += rotate.x * scaledRotateSpeed;
            if (_camera.m_Lens.FieldOfView == 40)
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
