using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShadowChimera
{
    public class SwitchCamera : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActionAsset;
        private InputAction _switch;

        [SerializeField] private CinemachineVirtualCamera _camMove;
        [SerializeField] private CinemachineVirtualCamera _camAim;


        private bool m_Pressed;

        protected void OnEnable()
        {
            _switch.performed += ctx => m_Pressed = true;
            _switch.canceled += ctx => m_Pressed = false;
        }

        void Awake()
        {

            _switch = _inputActionAsset.FindActionMap("Player").FindAction("SwitchCamera");
        }

        void Update()
        {
            if(m_Pressed)
            {
                (_camMove.Priority,_camAim.Priority) = (_camAim.Priority, _camMove.Priority);
            }
        }
    }
}
