using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace FragileReflection
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _stepSounds;
        [SerializeField] private AudioSource _mainSource;
        [SerializeField] private AudioSource _stepSource;
        public void Step()
        {
            int _currentIndex = Random.Range(0, _stepSounds.Count);
            _stepSource.PlayOneShot(_stepSounds[_currentIndex], 0.5f);
        }
    }
}
