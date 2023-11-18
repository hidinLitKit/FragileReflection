using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class LetterInteract : Interactable
    {
        private Letter letter;
        private Collider col;
        private void Awake()
        {
            letter = GetComponent<Letter>();
            col = GetComponent<Collider>();
        }

        public override void Interact()
        {
            letter.letterOpen();
            col.enabled = false;
        }
        public override string GetDescription()
        {
            return "читать";
        }
    }
}
