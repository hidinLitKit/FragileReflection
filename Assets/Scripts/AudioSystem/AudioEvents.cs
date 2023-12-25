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
        public AudioSource doorAudio => _doorAudio;
        public AudioSource saveAudio => _saveAudio;

        [SerializeField] private AudioSource _saveAudio;
        [SerializeField] private AudioSource _doorAudio;
        [SerializeField] private AudioSource _weaponEquipAudio;
        [SerializeField] private AudioSource _keyUseAudio;
        [SerializeField] private AudioSource _pickAudio;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            instance = this;
        }
        public void PlaySound(AudioSource source, AudioClip clip)
        {
            if (clip == null) return;
            source.clip = clip;
            source.Play();
        }
        public void PlaySound(AudioSource source)
        {
            source.Play();
        }
    }
}
