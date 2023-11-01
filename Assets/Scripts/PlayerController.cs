using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace ShadowChimera
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset m_inputActionAsset;
        [SerializeField] private CharacterController m_characterController;
        [SerializeField] private float moveSpeed = 5f;

        private InputActionMap m_playerMap;
        private InputAction m_moveAction;
        private void Awake()
        {
            m_playerMap = m_inputActionAsset.FindActionMap("Player");
            m_moveAction = m_playerMap.FindAction("Move");

            var move = m_moveAction.ReadValue<Vector2>();
            //в старой равносильно var horiz = Input.GetAxis("Horizontal") + vertical (тут два в одном).
        }
        private void OnEnable()
        {
            m_playerMap.Enable();
            //можна на встроенный ивент подписать функции (оч полезно)
            //m_moveAction.canceled += onMoveActionStarted;
        }
        private void OnDisable()
        {
            m_playerMap.Disable();
        }
        private void Update()
        {
            //типа геймпад 
            //var b = Gamepad.current.leftStick.ReadValue<Vector2>();

            var move = m_moveAction.ReadValue<Vector2>();
            if (move != Vector2.zero)
            {
                Console.WriteLine(move);
                var dir = new Vector3(move.x, 0, move.y);
                m_characterController.SimpleMove(dir*moveSpeed);
            }
        }
    } 
}
