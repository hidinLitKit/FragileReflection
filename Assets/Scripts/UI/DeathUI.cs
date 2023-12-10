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
            _deathUI.SetActive(true);
        }

        private void ShowDeathUIClose()
        {
            _deathUI.SetActive(false);
        }

        public void GotoFistLVL()
        {
            SceneManager.LoadScene("Main");
        }

        public void GotoMainMenu()
        {
            SceneManager.LoadScene("Main menu");
        }
    }
}
