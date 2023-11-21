using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Reflection;
using UnityEngine.UIElements;

namespace FragileReflection
{
    public abstract class NarrativeTalker : MonoBehaviour
    {
        protected GameObject primareUI;
        protected TextMeshProUGUI mainTextField;
        //Основной UI

        public List<string> NarrativeText;

        [Range(0f, 2f)]
        public float textSpeedPerSeconds;


        protected bool isFinished;

        protected int textIndex;


        public void StartNarration(GameObject controllerUI, List<string> textArray, TextMeshProUGUI textField)
        {
            textIndex = 0;
            controllerUI.SetActive(true);
            textField.text = "";
            StartCoroutine(TypeLine(textArray, textField));
        }
        public void EndNarration(GameObject controllerUI, TextMeshProUGUI textField)
        {
            controllerUI.SetActive(false);
            textField.text = "";
            isFinished = true;
            StopAllCoroutines();
        }
        public void NextLine(GameObject controllerUI, List<string> textArray, TextMeshProUGUI textField)
        {
            if (textIndex < textArray.Count - 1)
            {
                textIndex++;
                textField.text = string.Empty;
                StartCoroutine(TypeLine(textArray, textField));
            }
            else
            {
                EndNarration(controllerUI, textField);
            }
        }
        public IEnumerator TypeLine(List<string> textArray, TextMeshProUGUI textField)
        {
            foreach (char c in textArray[textIndex].ToCharArray())
            {
                textField.text += c;
                yield return new WaitForSeconds(textSpeedPerSeconds);
            }
        }

    }
}
