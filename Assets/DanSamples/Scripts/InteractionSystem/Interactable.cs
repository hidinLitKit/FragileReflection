using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dan
{
    public abstract class Interactable : MonoBehaviour
    {
        public enum InteractionType
        {
            Click,
            Hold,
            Read
        }
        float holdTime;
        public InteractionType interactionType;
        public abstract string GetDescription();
        public abstract void Interact();
        public void IncreaseHoldTime() => holdTime += Time.deltaTime;
        public void ResetHoldTime() => holdTime = 0f;

        public float GetHoldTime() => holdTime;
    }
}
