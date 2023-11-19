using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FragileReflection
{
    public class InteractionCast : MonoBehaviour
    {

        [SerializeField] private PlayerInteraction _playerInteraction;
        [SerializeField] private LayerMask _layerMask; //ignore every object not marked as Interactable or Walls Layer
        
        private CharacterController _characterController;
        
        Vector3 p1, p2;
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }
        private void Update()
        {
            RaycastHit hit;
            bool successfulHit = false;
            p1 = _characterController.center + transform.position + Vector3.up * - _characterController.height * 0.5f;
            p2 = p1 + _characterController.height * Vector3.up;
            if (Physics.CapsuleCast(p1, p2, _characterController.radius+0.5f, transform.forward, out hit, 2f, _layerMask))
            {
                //Debug.Log(hit.transform.name);
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable == null) return;
                successfulHit = true;
                GameEvents.InteractionEnter(interactable);
                _playerInteraction.HandleInteraction(interactable);
            }
            if(!successfulHit)
            {
                GameEvents.InteractionExit();
            }

        }

        
    }
}
