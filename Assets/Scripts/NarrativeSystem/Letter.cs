using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class Letter : NarrativeTalker
    {
        public static event System.Action<bool> letterAction;
        private bool _isActive = false;
        public void letterOpen()
        {
            _isActive = true;
            GameEvents.SwitchMap("UI","");
            letterAction?.Invoke(true);
            base.StopAllCoroutines();
            textIndex = 0;
            NarrativeData.instance.letterTextField.text = "";
            StartCoroutine(TypeLine(NarrativeText, NarrativeData.instance.letterTextField));
            

        }
        public void letterClose()
        {
            _isActive = false;
            GameEvents.SwitchMap("Player", "");
            letterAction?.Invoke(false);
        }
        private void OnEnable()
        {
            Debug.Log(NarrativeData.instance);
            NarrativeData.instance.letterNext.onClick.AddListener(buttonNext);
            NarrativeData.instance.letterPrev.onClick.AddListener(buttonBack);
            NarrativeData.instance.letterClose.onClick.AddListener(letterClose);

            letterAction += updateUI;
        }
        private void OnDisable()
        {
            NarrativeData.instance.letterNext.onClick.RemoveListener(buttonNext);
            NarrativeData.instance.letterPrev.onClick.RemoveListener(buttonBack);
            NarrativeData.instance.letterClose.onClick.RemoveListener(letterClose);

            letterAction -= updateUI;
        }
        
        public void buttonNext()
        {
            if (textIndex == NarrativeText.Count - 1 || !_isActive) return;
            textIndex++;
            base.StopAllCoroutines();
            NarrativeData.instance.letterTextField.text = "";
            StartCoroutine(TypeLine(NarrativeText, NarrativeData.instance.letterTextField));
        }
        public void buttonBack() 
        {
            if (textIndex == 0 || !_isActive) return;
            textIndex--;
            base.StopAllCoroutines();
            NarrativeData.instance.letterTextField.text = "";
            StartCoroutine(TypeLine(NarrativeText, NarrativeData.instance.letterTextField));
        }
        private void updateUI(bool isActive)
        {
            NarrativeData.instance.letterUI.SetActive(isActive);
        }
    }
}
