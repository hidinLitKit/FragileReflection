using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace FragileReflection
{
    public class VolumeSettings : MonoBehaviour
    {
        [Header ("Миксер")]
        [SerializeField] private AudioMixer _mixer;

        [Header("Слайдеры")]
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;

        void SetSliders()
        {
            _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        void Start()
        {
            if (PlayerPrefs.HasKey("MasterVolume"))
            {
                _mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
                _mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
                _mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
                SetSliders();
            }
            else
                SetSliders();
        }

        public void UpdateMasterVolume()
        {
            _mixer.SetFloat("MasterVolume", _masterSlider.value);
            PlayerPrefs.SetFloat("MasterVolume", _masterSlider.value);
        }

        public void UpdateMusicVolume()
        {
            _mixer.SetFloat("MusicVolume", _musicSlider.value);
            PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
        }

        public void UpdateSFXVolume()
        {
            _mixer.SetFloat("SFXVolume", _sfxSlider.value);
            PlayerPrefs.SetFloat("SFXVolume", _sfxSlider.value);
        }
    }
}
