using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FragileReflection
{
    public class AudioButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
    {
        [SerializeField] private AudioSource mainFx;
        [SerializeField] private AudioClip hoverFx;
        [SerializeField] private AudioClip clickFx;
        [SerializeField] private AudioClip someFx;

        public void HoverSound()
        {
            mainFx.PlayOneShot(hoverFx);
        }

        public void ClickSound()
        {
            mainFx.PlayOneShot(clickFx);
        }

        public void PlaySound()
        {
            mainFx.PlayOneShot(someFx);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            HoverSound();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ClickSound();
        }

    }
}
