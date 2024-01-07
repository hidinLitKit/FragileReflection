using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class MusicTrigger : AudioTrigger
    {
        [SerializeField] private AudioClip _musicClip;
        public override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            MusicManager.instance.PlayTrack(_musicClip);
        }
    }
}
