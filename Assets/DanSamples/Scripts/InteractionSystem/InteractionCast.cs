using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dan
{
    public class InteractionCast : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _action;
        [SerializeField] private LayerMask _layerMask;
        private int _layerM;
        private InputAction _interAct;
        private CharacterController _characterController;
        
        Vector3 p1, p2;
        private void Awake()
        {
            _layerM = ~_layerMask;
            _interAct = _action.FindActionMap("Player").FindAction("Interact");
            _characterController = GetComponent<CharacterController>();
        }
        private void Update()
        {
            RaycastHit hit;
            bool successfulHit = false;
            p1 = _characterController.center + transform.position + Vector3.up * - _characterController.height * 0.5f;
            p2 = p1 + _characterController.height * Vector3.up;
            if (Physics.CapsuleCast(p1, p2, _characterController.radius, transform.forward, out hit, 2.5f, ~_layerM))
            {
                Debug.Log(hit.transform.name);
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable == null) return;
                successfulHit = true;
                GameEvents.InteractionEnter(interactable);
                HandleInteraction(interactable);
            }
            if(!successfulHit)
            {
                GameEvents.InteractionExit();
            }

        }

        private void HandleInteraction(Interactable interactable)
        {
            switch(interactable.interactionType)
            {
                case Interactable.InteractionType.Click:
                    if(_interAct.IsPressed())
                        interactable.Interact();
                    break;

                case Interactable.InteractionType.Hold:

                    break;

                case Interactable.InteractionType.Read: 
                    
                    break;

                default: 
                    
                    break;
            }
        }
    }
}
