using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FragileReflection
{
    public static class GameEvents
    {
        //interaction
        public static event System.Action<Interactable> onInteractionEnter;
        public static event System.Action onInteractionExit;

        //Weapon
        public static event System.Action onFire;
        public static event System.Action onWeaponChanged;
        public static event System.Action<bool> onAiming;
        public static event System.Action onHealthImg;
        //PlayerInfo
        public static System.Action<float> onMedkitUse;
        public static System.Action<float> onMaxHealthIncrease;
        //Items
        public static System.Action<KeyObject> onKeyUse;
        public static System.Action<int> onSuccesUse;
        //ActionMaps
        public static event System.Action<string> onMapSwitched;
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


        //может понадобится, хз
        public static void HealthImage() 
        { 
            onHealthImg?.Invoke();
        }


        public static void SwitchMap(string map)
        {
            onMapSwitched?.Invoke(map);
        }

        public static void UseMedkit(float hp)
        {
            onMedkitUse?.Invoke(hp);
        }
        public static void IncreaseMaxHP(float hp)
        {
            onMaxHealthIncrease?.Invoke(hp);
        }
        public static void UseKey(KeyObject key)
        {
            onKeyUse?.Invoke(key);
        }
        public static void UseSuccess(int id)
        {
            onSuccesUse?.Invoke(id);
        }
    }
}
