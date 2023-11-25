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
        private Interactable _currentInteractable;
        private void Awake()
        {
            _interAct = _action.FindActionMap("Player").FindAction("Interact");
            _currentInteractable = null;
        }
        private void OnEnable()
        {
            GameEvents.onKeyUse += HandleKey;
            GameEvents.onInteractionEnter += getInteractable;
            GameEvents.onInteractionExit += getInteractable;
        }
        private void OnDisable()
        {
            GameEvents.onKeyUse -= HandleKey;
            GameEvents.onInteractionEnter -= getInteractable;
            GameEvents.onInteractionExit -= getInteractable;

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
        private void HandleKey(KeyObject key)
        {
            if (_currentInteractable == null) return;
            if(_currentInteractable.gameObject.TryGetComponent<ItemRequire>(out ItemRequire itemReq))
            {
                if(key.ID == itemReq.Key.ID)
                {
                    itemReq.ChangeLockState();
                    GameEvents.UseSuccess(key.ID);
                }
            }
        }
        private void getInteractable()
        {
            _currentInteractable = null;
        }
        private void getInteractable(Interactable inter)
        {
            _currentInteractable = inter;
        }
        

    }
}
