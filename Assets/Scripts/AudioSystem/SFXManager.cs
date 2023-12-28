using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class SFXManager : MonoBehaviour
    {
        public static SFXManager instance { get; private set; }

        [SerializeField] private AudioSource _source;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            instance = this;
        }
        public void PlaySound(AudioClip clip)
        {
            if (clip == null) return;
            _source.PlayOneShot(clip);
        }
        public void PlaySound()
        {
            _source.PlayOneShot(_source.clip);
        }
    }
}
