using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

namespace FragileReflection
{
    public class StepsAudioManager : MonoBehaviour
    {
        [SerializeField] private EnemyController enemyController;
        [SerializeField] private List<AudioClip> audioClips;
        [SerializeField] private AudioSource audioSource;

        private int _currentIndex = -1;

        public void step()
        {
            _currentIndex = _currentIndex >= audioClips.Count-1 ? 0 : _currentIndex + 1;
            audioSource.PlayOneShot(audioClips[_currentIndex]);
        }

        //public void scream()
        //{
        //    enemyController._audioController.PlayAudio(false, EnemySounds.PreAttack, true);
        //}

        //public void stuggleSound()
        //{
        //    enemyController._audioController.PlayAudio(false, EnemySounds.PreAttack, true);
        //}
    }
}
  