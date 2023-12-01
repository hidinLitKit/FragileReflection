using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class ShowHideUI : MonoBehaviour
    {
        [SerializeField] private GameObject _fullUI;

        private void OnEnable()
        {
            GameEvents.onStatusUI += ShowUI;
        }

        private void OnDisable()
        {
            GameEvents.onStatusUI -= ShowUI;
        }

        private void ShowUI(bool status)
        {
            if (status)
                _fullUI.gameObject.SetActive(true);
            else
                _fullUI.gameObject.SetActive(false);
        }
    }
}
