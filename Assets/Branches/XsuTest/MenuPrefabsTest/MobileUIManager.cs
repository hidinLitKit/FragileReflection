using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class MobileUIManager : MonoBehaviour
    {
        bool isAndroid = false;
        [SerializeField] GameObject AndroidUI;
        [SerializeField] GameObject UIInventory;
        [SerializeField] GameObject UIController;
        void Awake()
        {
#if UNITY_ANDROID
    isAndroid = true;
#endif
            if(isAndroid)
                AndroidUI.SetActive(true);
        }

        private void OnEnable()
        {
            if(isAndroid)
            {
                GameEvents.onInventoryUI += SetActiveInventory;
                GameEvents.onGamePause += SetActiveController;
            }
                
        }

        private void OnDisable()
        {
            if (isAndroid)
            {
                GameEvents.onInventoryUI -= SetActiveInventory;
                GameEvents.onGamePause += SetActiveController;
            }
        }

        void SetActiveController(bool activate)
        {
            AndroidUI.SetActive(!activate);
        }

        void SetActiveInventory(bool activate)
        {
            UIInventory.SetActive(activate);
            UIController.SetActive(!activate);
        }
    }
}
