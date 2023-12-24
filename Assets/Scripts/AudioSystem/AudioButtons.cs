using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace FragileReflection
{
    [System.Serializable]
    public class ButtonElement
    {
        [SerializeField] private Button _button;
        [SerializeField] private AudioSource _clickSound;
        public void Initialize()
        {
            _button.onClick.AddListener(_clickSound.Play);
        }
        public void Purify()
        {
            _button.onClick.RemoveListener(_clickSound.Play);
        }
    }
    public class AudioButtons : MonoBehaviour
    {
        public List<ButtonElement> buttons;
        private void OnEnable()
        {
            foreach(ButtonElement btt in buttons)
            {
                if (btt == null) break;
                btt.Initialize();

            }
        }
        private void OnDisable()
        {
            foreach (ButtonElement btt in buttons)
            {
                if (btt == null) break;
                btt.Purify();
            }
        }
    }
}
