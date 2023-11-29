using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class StepsAudioManager : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> audioClips;
        [SerializeField] private AudioSource audioSource;

        private int _currentIndex = -1;

        public void step()
        {
            _currentIndex = _currentIndex >= audioClips.Count-1 ? 0 : _currentIndex + 1;
            audioSource.PlayOneShot(audioClips[_currentIndex]);
        }
    }
}
  