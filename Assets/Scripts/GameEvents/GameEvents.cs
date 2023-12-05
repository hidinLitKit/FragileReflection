using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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
        public static event System.Action onWeaponReload;
        public static event System.Action<bool> onAiming;

        //PlayerInfo
        public static System.Action<float> onMedkitUse;
        public static System.Action<float> onMaxHealthIncrease;
        //public static Action<bool> onStatusUI;
        public static event Action<string, int, string, Color> onHealthStatusChanged;
        public static event Action<bool> onInventoryUI;

        //Items
        public static System.Action<KeyObject> onKeyUse;
        public static System.Action<int> onSuccesUse;
        public static System.Action onPickItem;

        //ActionMaps
        public static event System.Action<string> onMapSwitched;

        //Stamina
        public static event Action<float> onStaminaUsed;
        public static event Action<float> onStaminaRegenerated;
        public static Action onStaminaUIOpen;
        public static Action onStaminaUIClose;
        //Parallel World
        public static event System.Action onParallelWorldActive;

        //UI
        public static event Action onUIFade;
        public static System.Action<float> onInteractionProgress;

        //Pause
        public static event Action<bool> onGamePause;

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
        public static void WeaponReload()
        {
            onWeaponReload?.Invoke();
        }

        //public static void StatusControl(bool status)
        //{
        //    onStatusUI?.Invoke(status);
        //}

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
        public static void InventoryUIAble(bool show)
        {
            onInventoryUI?.Invoke(show);
        }
        public static void UseKey(KeyObject key)
        {
            onKeyUse?.Invoke(key);
        }
        public static void UseSuccess(int id)
        {
            onSuccesUse?.Invoke(id);
        }
        public static void PickItem()
        {
            onPickItem?.Invoke();
        }
        public static void StaminaUsed(float amount)
        {
            onStaminaUsed?.Invoke(amount);
        }

        public static void StaminaRegenerated(float amount)
        {
            onStaminaRegenerated?.Invoke(amount);
        }

        public static void StaminaUIOpen()
        {
            onStaminaUIOpen?.Invoke();
        }

        public static void StaminaUIClose()
        {
            onStaminaUIClose?.Invoke();
        }

        public static void HealthChange(string pulse, int fps, string status, Color color)
        {
            onHealthStatusChanged?.Invoke(pulse, fps, status, color);
        }

        public static void ActiveParallelWorld()
        {
            onParallelWorldActive?.Invoke();
        }
        
        public static void UIFade()
        {
            onUIFade?.Invoke();
        }

        public static void GamePause(bool status)
        {
            onGamePause?.Invoke(status);
            }
        public static void InteractionProgress(float f)
        {
            onInteractionProgress?.Invoke(f);
        }
        
    }
}
