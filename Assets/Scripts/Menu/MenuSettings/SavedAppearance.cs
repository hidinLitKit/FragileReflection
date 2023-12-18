using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FragileReflection
{
    public class SavedAppearance : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private readonly float _fadeInTime = 1.0f;
        private readonly float _displayTime = 1.0f;
        private readonly float _fadeOutTime = 1.0f;

        public void OptionSave()
        {
            StartCoroutine(FadeInOut());
        }

        IEnumerator FadeInOut()
        {
            // Появление
            float elapsedTime = 0f;
            Color startColor = _text.color;
            Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

            while (elapsedTime < _fadeInTime)
            {
                _text.color = Color.Lerp(startColor, targetColor, elapsedTime / _fadeInTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(_displayTime);

            // Исчезновение
            elapsedTime = 0f;
            startColor = _text.color;
            targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

            while (elapsedTime < _fadeOutTime)
            {
                _text.color = Color.Lerp(startColor, targetColor, elapsedTime / _fadeOutTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _text.color = targetColor;
        }
    }
}
