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
        private void OnEnable()
        {
            Letter.letterAction += manageCollider;
        }
        private void OnDisable()
        {
            Letter.letterAction -= manageCollider;
        }
        public override void Interact()
        {
            letter.letterOpen();
            
        }
        public override string GetDescription()
        {
            return "читать";
        }
        private void manageCollider(bool colBool)
        {
            col.enabled = !colBool;
        }
    }
}
