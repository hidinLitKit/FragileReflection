using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class AudioButton : MonoBehaviour
    {
        [SerializeField] private AudioSource mainFx;
        [SerializeField] private AudioClip hoverFx;
        //[SerializeField] private AudioClip clickFx;

        public void HoverSound()
        {
            mainFx.PlayOneShot(hoverFx);
        }

        // public void ClickSound()
        // {
        //     mainFx.PlayOneShot(clickFx);
        // }

    }
}
