using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace FragileReflection
{
    public class VolumeSettings : MonoBehaviour
    {
        private static readonly string FirstPlay = "FirstPlay";
        private static readonly string MusicPref = "MusicPref";
        private static readonly string SoundEffectsPref = "SoundEffectsPref";
        private int firstPlayInt;
        [SerializeField] private Slider _musicSlider, _soundEffectsSlider;
        private float musicFloat, soundEffectsFloat;
        [SerializeField] private AudioSource _musicAudio;
        [SerializeField] private AudioSource[] _soundEffectsAudio;

        private void Start()
        { 
            firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

            if (firstPlayInt == 0)
            {
                musicFloat = 0.25f;
                soundEffectsFloat = 0.75f; 
                _musicSlider.value = musicFloat;
                _soundEffectsSlider.value = soundEffectsFloat;
                PlayerPrefs.SetFloat(MusicPref, musicFloat);
                PlayerPrefs.SetFloat(SoundEffectsPref, soundEffectsFloat); 
                PlayerPrefs.SetInt(FirstPlay, -1);
            }
            else
            {
                musicFloat = PlayerPrefs.GetFloat(MusicPref); 
                _musicSlider.value = musicFloat;
                soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
                _soundEffectsSlider.value = soundEffectsFloat;
            }
        }
        
        public void SaveSoundSettings()
        {
            PlayerPrefs.SetFloat(MusicPref, _musicSlider.value);
            PlayerPrefs.SetFloat(SoundEffectsPref, _soundEffectsSlider.value);
        }

        void OnApplicationFocus(bool inFocus)
        {
            if (!inFocus)
                SaveSoundSettings();
        }

        public void UpdateSound()
        {
            _musicAudio.volume = _musicSlider.value;

            for (int i = 0; i < _soundEffectsAudio.Length; i++)
            {
                _soundEffectsAudio[i].volume = _soundEffectsSlider.value;
            }
        }
    }
}
