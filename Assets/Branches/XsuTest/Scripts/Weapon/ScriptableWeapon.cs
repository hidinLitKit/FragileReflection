using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FirearmsWeapon", order = 1)]
    public class ScriptableWeapon : ScriptableObject
    {
        [Header("Урон")]
        public float HeadDamage;
        public float BodyDamage;

        [Space]
        public float RateOfFire;
        public float RechargeSpeed;
        public float XRecoil;
        public float YRecoil;

        [Space]
        public int ShotAmount;

        public Sprite CrossHair;

    }
}
