using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FragileReflection
{
    public class DeathUI : MonoBehaviour
    {
        [SerializeField] private GameObject _deathUI;

        private void OnEnable()
        {
            GameEvents.onDeathUIOpen += ShowDeathUIOpen;
            GameEvents.onDeathUIClose += ShowDeathUIClose;
        }

        private void OnDisable()
        {
            GameEvents.onDeathUIOpen -= ShowDeathUIOpen;
            GameEvents.onDeathUIClose -= ShowDeathUIClose;
        }

        private void ShowDeathUIOpen()
        {
            GameEvents.InventoryUIAble(false);
            GameEvents.StaminaUIClose();
            _deathUI.SetActive(true);
        }

        private void ShowDeathUIClose()
        {
            _deathUI.SetActive(false);
        }

        public void Continue()
        {
            SceneManager.LoadScene("Main");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("Main menu");
        }
    }
}
