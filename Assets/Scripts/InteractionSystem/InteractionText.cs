using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace FragileReflection
{
    public class InteractionText : MonoBehaviour
    {
        [SerializeField] private GameObject _interactionUI;
        [SerializeField] private TextMeshProUGUI _interactionText;
        [SerializeField] private Image _interactionHold;
        private void OnEnable()
        {
            GameEvents.onInteractionEnter += showUI;
            GameEvents.onInteractionExit += hideUI;
            GameEvents.onInteractionProgress += fillBar;
        }

        private void OnDisable()
        {
            GameEvents.onInteractionEnter -= showUI;
            GameEvents.onInteractionExit -= hideUI;
            GameEvents.onInteractionProgress += fillBar;
        }

        private void showUI(Interactable interactable)
        {
            _interactionUI.SetActive(true); 
            _interactionText.text = interactable.GetDescription();
        }
        private void hideUI() 
        {
            _interactionUI.SetActive(false);
            _interactionText.text = " ";
        }
        private void fillBar(float f)
        {
            _interactionHold.fillAmount = f;
        }
    }
}
