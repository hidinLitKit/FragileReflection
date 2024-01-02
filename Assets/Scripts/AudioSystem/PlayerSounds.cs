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
        [SerializeField] private List<AudioClip> _staminaBreath;
        [SerializeField] private AudioClip _hitClip;
        [SerializeField] private AudioClip _aimBreath;
        [SerializeField] private AudioSource _2dSource;
        [SerializeField] private AudioSource _3dSource;

        [SerializeField] private float aimCDseconds = 10f;
        [SerializeField] private float staminaCDseconds = 10f;

        private bool _aimCD = false;
        private bool _staminaCD = false;
        public void Step()
        {
            int _currentIndex = Random.Range(0, _stepSounds.Count);
            _3dSource.PlayOneShot(_stepSounds[_currentIndex], 0.5f);
        }
        public void AttackStuggle()
        {
            int _stuggleChance = Random.Range(0, 100);
            if( _stuggleChance > 50)
            {
                int _currentIndex = Random.Range(0, _attackStuggle.Count);
                _2dSource.PlayOneShot(_attackStuggle[_currentIndex]);
            }   
        }
        public void TakeHit()
        {
            _2dSource.PlayOneShot(_hitClip);
        }
        public void AimIdle()
        {
            if (_aimCD) return;
            _2dSource.PlayOneShot(_aimBreath);
            _aimCD = true;
            StartCoroutine(AimIdleCD());
        }
        public void StaminaBreath()
        {
            if (_staminaCD) return;
            _2dSource.PlayOneShot(_staminaBreath[Random.Range(0, _staminaBreath.Count)]);
            _staminaCD = true;
            StartCoroutine(StaminaBreathCD());
        }
        private IEnumerator AimIdleCD()
        {
            yield return new WaitForSeconds(aimCDseconds);
            _aimCD = false;
        }
        private IEnumerator StaminaBreathCD()
        {
            yield return new WaitForSeconds(staminaCDseconds);
            _staminaCD = false;
        }
    }
}
