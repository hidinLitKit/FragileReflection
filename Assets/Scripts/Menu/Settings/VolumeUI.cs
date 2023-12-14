using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace FragileReflection
{
    public class VolumeUI : MonoBehaviour
    {
        public AudioMixer mixer;
        public Slider masterSlider;
        public Slider musicSlider;
        public Slider sfxSlider;

        void SetSliders()
        {
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        void Start()
        {
            if (PlayerPrefs.HasKey("MasterVolume"))
            {
                mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
                mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
                mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
                SetSliders();
            }
            else
                SetSliders();
        }

        public void UpdateMasterVolume()
        {
            mixer.SetFloat("MasterVolume", masterSlider.value);
            PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
        }

        public void UpdateMusicVolume()
        {
            mixer.SetFloat("MusicVolume", musicSlider.value);
            PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        }

        public void UpdateSFXVolume()
        {
            mixer.SetFloat("SFXVolume", sfxSlider.value);
            PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        }
    }
}
