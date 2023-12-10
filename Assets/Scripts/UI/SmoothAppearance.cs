using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class SmoothAppearance : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            _canvasGroup.alpha = 0f;

            StartCoroutine(FadeInCanvas(2f));
        }

        private IEnumerator FadeInCanvas(float duration)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                _canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            _canvasGroup.alpha = 1f;
        }
    }
}
