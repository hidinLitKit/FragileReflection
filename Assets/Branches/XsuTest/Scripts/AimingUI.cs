using FragileReflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;
using UnityEngine.UI;

namespace FragileReflection
{
    public class AimingUI : MonoBehaviour
    {
        [SerializeField] private Image _crossHairImage;

        private void Awake()
        {
            ShowUI(false);
        }

        private void OnEnable()
        {
            GameEvents.onWeaponChanged += ChangeCrossHairUI;
            GameEvents.onAiming += ShowUI;
        }

        private void OnDisable()
        {
            GameEvents.onWeaponChanged -= ChangeCrossHairUI;
            GameEvents.onAiming -= ShowUI;
        }

        private void ChangeCrossHairUI()
        {
            _crossHairImage.sprite = WeaponManager.currentWeapon.WeaponType.CrossHair;
        }

        private void ShowUI(bool visible)
        {
            int alpha = visible ? 255 : 0;
            _crossHairImage.color = new Color( 255, 255, 255, alpha);
        }
    }
}
