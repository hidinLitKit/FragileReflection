using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FragileReflection
{
    public static class GameEvents
    {
        public static event System.Action<Interactable> onInteractionEnter;
        public static event System.Action onInteractionExit;

        public static event System.Action onFire;
        public static event System.Action onWeaponChanged;
        public static event System.Action<bool> onAiming;

        public static void InteractionEnter(Interactable interactable)
        {
            onInteractionEnter?.Invoke(interactable);
        }
        public static void InteractionExit()
        {
            onInteractionExit?.Invoke();
        }

        public static void Fire()
        {
            onFire?.Invoke();
        }

        public static void ChangeWeapon()
        {
            onWeaponChanged?.Invoke();
        }

        public static void Aim(bool aiming)
        {
            onAiming?.Invoke(aiming);
        }

    }
}
