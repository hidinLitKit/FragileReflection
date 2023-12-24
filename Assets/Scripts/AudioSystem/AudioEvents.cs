using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class AudioEvents : MonoBehaviour
    {
        public static AudioEvents instance { get; private set; }
        public AudioSource keyUseAudio => _keyUseAudio;
        public AudioSource pickAudio => _pickAudio;
        public AudioSource weaponEquipAudio => _weaponEquipAudio;

        [SerializeField] private AudioSource _weaponEquipAudio;

        [SerializeField] private AudioSource _keyUseAudio;
        [SerializeField] private AudioSource _pickAudio;
        [SerializeField] private AudioSource exitAudio;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            instance = this;
        }
        private void OnEnable()
        {
            GameEvents.onExit += exitAudio.Play;
        }
        private void OnDisable()
        {
            GameEvents.onExit -= exitAudio.Play;
        }
        public void PlaySound(AudioSource source, AudioClip clip)
        {
            if (clip == null) return;
            source.clip = clip;
            source.Play();
        }
    }
}
