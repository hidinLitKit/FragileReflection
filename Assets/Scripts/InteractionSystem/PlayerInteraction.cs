using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FragileReflection
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _action;
        private InputAction _interAct;
        private void Awake()
        {
            _interAct = _action.FindActionMap("Player").FindAction("Interact");
        }
        public void HandleInteraction(Interactable interactable)
        {
            switch (interactable.interactionType)
            {
                case Interactable.InteractionType.Click:
                    if (_interAct.WasPressedThisFrame())
                        interactable.Interact();
                    break;

                case Interactable.InteractionType.Hold:

                    break;
                default:

                    break;
            }
        }
    }
}
