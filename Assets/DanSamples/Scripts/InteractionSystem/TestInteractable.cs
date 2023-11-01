using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dan
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
