using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

namespace FragileReflection
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _stepSounds;
        [SerializeField] private List<AudioClip> _attackStuggle;
        [SerializeField] private AudioClip _hitClip;
        [SerializeField] private AudioClip _aimBreath;
        [SerializeField] private AudioSource _mainSource;
        [SerializeField] private AudioSource _stepSource;

        private bool _aimCD = false;
        public void Step()
        {
            int _currentIndex = Random.Range(0, _stepSounds.Count);
            _stepSource.PlayOneShot(_stepSounds[_currentIndex], 0.5f);
        }
        public void AttackStuggle()
        {
            int _stuggleChance = Random.Range(0, 100);
            if( _stuggleChance > 50)
            {
                int _currentIndex = Random.Range(0, _attackStuggle.Count);
                _mainSource.PlayOneShot(_attackStuggle[_currentIndex]);
            }   
        }
        public void TakeHit()
        {
            _mainSource.PlayOneShot(_hitClip);
        }
        public void AimIdle()
        {
            if (_aimCD) return;
            _mainSource.PlayOneShot(_aimBreath);
            _aimCD = true;
            StartCoroutine(breathCD(10));
        }
        private IEnumerator breathCD(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            _aimCD = false;
        }
    }
}
