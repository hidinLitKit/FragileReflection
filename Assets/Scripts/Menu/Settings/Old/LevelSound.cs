using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class LevelSound : MonoBehaviour
    {
        private static readonly string MusicPref = "MusicPref";
        private static readonly string SoundEffectsPref = "SoundEffectsPref"; 
        private float musicFloat, soundEffectsFloat;
        [SerializeField] private AudioSource _musicAudio;
        [SerializeField] private AudioSource[] _soundEffectsAudio;

        private void Awake() 
        {
            LevelSoundSettings();
        }

        private void LevelSoundSettings()
        {
            musicFloat = PlayerPrefs.GetFloat(MusicPref); 
            soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);

            _musicAudio.volume = musicFloat;

            for (int i = 0; i < _soundEffectsAudio.Length; i++)
            {
                _soundEffectsAudio[i].volume = soundEffectsFloat;
            }
        }
        
    }
}
