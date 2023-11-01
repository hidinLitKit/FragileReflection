using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XsuTest
{
    public class InteractCast : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        Vector3 p1, p2;
        
        void LateUpdate()
        {
            RaycastHit hit;
            p1 = characterController.center + transform.position + Vector3.up*characterController.height*0.5f;
            p2 = p1 - characterController.height*Vector3.up*0.5f;

            if(Physics.CapsuleCast(p1, p2, characterController.radius, transform.forward, out hit, 5))
            {
                Debug.Log(hit.transform.name);
            }
        }
    }
}
