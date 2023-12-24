using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace FragileReflection
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private WeaponManager _weaponManager;
        [SerializeField] private AudioSource _weaponSounds;
    }
}
