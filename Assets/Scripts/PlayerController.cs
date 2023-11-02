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
        [SerializeField] private float moveSpeed = 5f;
        private float zoomSpeed = 20f;

        private InputActionMap _playerMap;
        private InputAction _moveAction;
        private InputAction _zoomAction;

        private void Awake()
        {
            _playerMap = _inputActionAsset.FindActionMap("Player");
            _moveAction = _playerMap.FindAction("Move");

            _zoomAction = _playerMap.FindAction("Zoom");
            _zoomAction.performed += OnMouseRightClickPerformed;
            _zoomAction.canceled += OnMouseRightClickCanceled;

            //var move = _moveAction.ReadValue<Vector2>();
            //в старой равносильно var horiz = Input.GetAxis("Horizontal") + vertical (тут два в одном).
        }

        private void OnEnable()
        {
            _playerMap.Enable();

            //можна на встроенный ивент подписать функции (оч полезно)
            //_moveAction.canceled += onMoveActionStarted;
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
            //типа геймпад 
            //var b = Gamepad.current.leftStick.ReadValue<Vector2>();
            var move = _moveAction.ReadValue<Vector2>();
            if (move != Vector2.zero)
            {
                Console.WriteLine(move);
                var dir = new Vector3(move.x, 0, move.y);
                _characterController.SimpleMove(dir * moveSpeed);
            }

        }

    } 
}
