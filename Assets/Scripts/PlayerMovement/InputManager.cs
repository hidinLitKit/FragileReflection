using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FragileReflection
{
    public class InputManager : MonoBehaviour
    {
        public PlayerInput playerInput;
        [SerializeField] private static PlayerInput _playerInput;
        private void Awake()
        {
            _playerInput = playerInput;
            GameEvents.SwitchMap("Player");
        }
        private void OnEnable()
        {
            GameEvents.onMapSwitched += ToogleActionMaps;
            GameEvents.onMapSwitched += CursorController;
        }
        private void OnDisable()
        {
            GameEvents.onMapSwitched -= ToogleActionMaps;
            GameEvents.onMapSwitched -= CursorController;
        }

        public void OnExit(InputValue value)
        {
            UIEvents.ExitPressed();
        }
        public void OnPrimary(InputValue value)
        {
            UIEvents.PrimaryPressed();
        }
        public void OnSecondary(InputValue value)
        {
            UIEvents.SecondaryPressed();
        }
        public void OnRight(InputValue value)
        {
            UIEvents.RightPressed();
        }
        public void OnLeft(InputValue value)
        {
            UIEvents.LeftPressed();
        }

        public static void ToogleActionMaps(string inputMap)
        {
            Debug.Log(inputMap + " enabled");
            _playerInput.SwitchCurrentActionMap(inputMap);
        }
        private void CursorController(string inputMap)
        {
            switch(inputMap)
            {
                case ("UI"):
                case ("DeathMap"):
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    //��� �������� �������, �� ���� ���, ����� �� �������� � ��������� 
                    // �������� � ���, ��� ����� ��������� ��������� ��������� �� ����� ������ (� ������ ����������� ��� �� UI)
                    // �� � UI ���� exit �� ����� �� ���� ����� ����, ������������� ������������ ����������. ��� � � ������ ������ ����� ����
                    break;
                default:
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;
            }
        }
    }
}
