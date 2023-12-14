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
        private CanvasGroup androidUI;
        private CanvasGroup inventoryUI;
        private CanvasGroup controllerUI;

        void Awake()
        {
    #if UNITY_ANDROID
            isAndroid = true;
    #endif
            if (!isAndroid) GetComponent<MobileUIManager>().enabled = false;
            AndroidUI.SetActive(true);
            androidUI = AndroidUI.GetComponent<CanvasGroup>();
            inventoryUI = UIInventory.GetComponent<CanvasGroup>();
            controllerUI = UIController.GetComponent<CanvasGroup>();
            CanvasSwitch(inventoryUI, false);

        }

        private void OnEnable()
        {
            GameEvents.onInventoryUI += SetActiveInventory;
            GameEvents.onGamePause += SetActiveController;
 
        }

        private void OnDisable()
        {
            GameEvents.onInventoryUI -= SetActiveInventory;
            GameEvents.onGamePause -= SetActiveController;

        }

        private void SetActiveController(bool activate)
        {
            CanvasSwitch(androidUI, !activate);

        }

        private void SetActiveInventory(bool activate)
        {
            CanvasSwitch(inventoryUI, activate);
            CanvasSwitch(controllerUI, !activate);
        }
        private void CanvasSwitch(CanvasGroup canv, bool state)
        {
            canv.alpha = state ? 1 : 0;
            canv.interactable = state;
            canv.blocksRaycasts = state;
        }
    }
}
