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
        public static void ToogleActionMaps(string inputMap)
        {
            Debug.Log(inputMap + " enabled");
            _playerInput.SwitchCurrentActionMap(inputMap);
        }
        public void OnExit(InputValue value)
        {
            GameEvents.SwitchMap("Player");
        }
        private void CursorController(string inputMap)
        {
            switch(inputMap)
            {
                case ("UI"):
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    //GameEvents.InventoryUIAble(true);
                    GameEvents.StaminaUIClose();
                    break;
                case ("DeathMap"):
                    //это наверное костыль, но пока так, нужно мб спросить у Валентина 
                    // проблема в том, что нужно запрещать двигаться персонажу во время смерти (и вообще переключать его на UI)
                    // но в UI есть exit на выход из этой инпут мапы, следовательно возвращается управление. Вот я и создал пустую инпут мапу
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
                default:
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    GameEvents.InventoryUIAble(false);
                    GameEvents.GamePause(false);
                    GameEvents.StaminaUIOpen();
                    break;
            }
        }
    }
}
