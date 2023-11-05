using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FragileReflection
{
    public class TestInteractable : Interactable
    {
        public override void Interact()
        {
            Debug.Log("CLICK");
        }
        public override string GetDescription()
        {
            return "test";
        }
    }
}
