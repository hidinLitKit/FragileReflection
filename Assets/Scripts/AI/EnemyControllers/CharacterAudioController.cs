using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public enum Sounds
    {
        PreAttack,
        Patrolling,
        Attacking,
        Stuggle,
        Death
    }
    public class CharacterAudioController : MonoBehaviour
    {
        [Header("AudioClips")]
        [SerializeField] private AudioClip preAttack;
        [SerializeField] private AudioClip patrolling;
        [SerializeField] private AudioClip attacking;
        [Header("Нанесение урона врагу")]
        [SerializeField] private AudioClip stuggle;
        [SerializeField] private AudioClip dying;

        protected AudioSource _audioSource;
        protected List<AudioClip> _audioClipList;
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioClipList = new List<AudioClip>() { preAttack, patrolling, attacking, stuggle, dying };
        }

        public void PlayAudio(bool loop, Sounds sound, bool checkIsPlaying = false)
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
