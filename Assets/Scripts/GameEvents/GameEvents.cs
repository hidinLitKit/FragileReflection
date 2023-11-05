using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShadowChimera
{
    public static class GameEvents
    {
        public static event System.Action<Interactable> onInteractionEnter;
        public static event System.Action onInteractionExit;

        public static void InteractionEnter(Interactable interactable)
        {
            onInteractionEnter.Invoke(interactable);
        }
        public static void InteractionExit()
        {
            onInteractionExit.Invoke();
        }

    }
}
