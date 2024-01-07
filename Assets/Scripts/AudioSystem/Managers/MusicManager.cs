using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace FragileReflection
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager instance { get; private set; }

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

        public void PlayTrack(AudioClip clip)
        {
            if(_source.isPlaying)
            {
                StopAllCoroutines();
                StartCoroutine(TrackFade(clip));
                return;
            }
            _source.clip = clip;
            _source.Play();

        }
        private IEnumerator TrackFade(AudioClip newClip)
        {
            _source.DOFade(0f, 3f);
            yield return new WaitForSeconds(3f);
            _source.clip = newClip;
            _source.DOFade(1f, 3f);
            _source.Play();

        }

    }
}
