using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FragileReflection
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _camera;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float lookSpeed = 2f;

        private InputActionMap _playerMap;
        private InputAction _moveAction;
        private InputAction _lookAction;

        private Vector2 lookInput;

        private void Awake()
        {
            _playerMap = _inputActionAsset.FindActionMap("Player");
            _moveAction = _playerMap.FindAction("Move");
            _lookAction = _playerMap.FindAction("Look");

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


            var look = _lookAction.ReadValue<Vector2>();
            lookInput += look * lookSpeed * Time.deltaTime;
            lookInput.y = Mathf.Clamp(lookInput.y, -90, 90);
            _camera.localRotation = Quaternion.Euler(-lookInput.y, 0, 0);
            transform.rotation = Quaternion.Euler(0, lookInput.x, 0);

        }

    } 
}
