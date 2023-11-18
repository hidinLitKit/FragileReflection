using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FragileReflection
{
    public class InputManager : MonoBehaviour
    {
        public static NewInput inputActions;
        private void Awake()
        {
            inputActions = new NewInput();
        }
        private void Start()
        {
            inputActions.Player.Enable();
        }
        public static void ToogleActionMaps(InputActionMap inputMap)
        {
            if (inputMap.enabled) return;
            Debug.Log(inputMap + "enabled");
            inputActions.Disable();
            inputMap.Enable();
        }
        public void OnExit(InputValue value)
        {
            ToogleActionMaps(inputActions.Player);
        }
    }
}
