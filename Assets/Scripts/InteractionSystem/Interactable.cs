using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FragileReflection
{
    public abstract class Interactable : MonoBehaviour
    {
        public enum InteractionType
        {
            Click,
            Hold
        }
        float holdTime;
        public InteractionType interactionType;
        public abstract string GetDescription();
        public abstract void Interact();
        public void IncreaseHoldTime() => holdTime += 0.1f*Time.deltaTime;
        public void ResetHoldTime() => holdTime = 0f;

        public float GetHoldTime() => holdTime;
        public void SetHoldTime(float f) => holdTime = f;

    }
}
