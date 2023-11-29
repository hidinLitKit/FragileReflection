using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public enum EnemySounds
    {
        PreAttack,
        Patrolling,
        Attacking,
        Stuggle,
        Death
    }
    public class EnemyAudioController : MonoBehaviour
    {
        [Header("AudioClips")]
        [SerializeField] private AudioClip preAttack;
        [SerializeField] private AudioClip patrolling;
        [SerializeField] private AudioClip attacking;
        [Header("Нанесение урона врагу")]
        [SerializeField] private AudioClip stuggle;
        [SerializeField] private AudioClip dying;

        private AudioSource _audioSource;
        private List<AudioClip> _audioClipList;
        
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioClipList = new List<AudioClip>() { preAttack, patrolling, attacking, stuggle };
        }

        public void PlayAudio(bool loop, EnemySounds sound, bool checkIsPlaying = false)
        {
            AudioClip currentAudio = _audioClipList[(int)sound];
            if (_audioSource.clip == currentAudio && _audioSource.isPlaying && checkIsPlaying)
                return;
            if (loop)
            {
                _audioSource.loop = true;
                _audioSource.clip = currentAudio;
                _audioSource.Play();
            }
            else
                _audioSource.PlayOneShot(currentAudio);
        }
    }
}
