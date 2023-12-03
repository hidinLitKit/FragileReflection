using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace FragileReflection
{
    public class UIAnims : MonoBehaviour
    {
        [SerializeField] private Image _FadeImage;
        private void OnEnable()
        {
            GameEvents.onUIFade += BlackFade;
        }
        private void OnDisable()
        {
            GameEvents.onUIFade -= BlackFade;
        }
        private void BlackFade()
        {
            StopAllCoroutines();
            _FadeImage.DOFade(0f,0.01f);
            StartCoroutine(fade());
        }
        IEnumerator fade()
        {
            _FadeImage.DOFade(1f, 0.5f);
            yield return new WaitForSeconds(0.5f);
            _FadeImage.DOFade(0f, 0.5f);
        }
    }
}
