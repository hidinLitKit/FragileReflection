using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FragileReflection
{
    public class PauseState : GameState
    {
		[SerializeField] private Button _continueButton;
        [SerializeField] private Button _settingsButton;
		[SerializeField] private Button _exitButton;

		protected override void OnEnable()
		{
			base.OnEnable();
			Time.timeScale = 0;
			GameEvents.SwitchMap(uiInputMap);
			
			UIEvents.onExit += Resume;
			_continueButton.onClick.AddListener(Resume);
			_settingsButton.onClick.AddListener(Settings);
			_exitButton.onClick.AddListener(GotoMainMenu);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			Time.timeScale = 1;
			GameEvents.SwitchMap(playerInputMap);
			
			UIEvents.onExit -= Resume;
			_continueButton.onClick.RemoveListener(Resume);
			_settingsButton.onClick.RemoveListener(Settings);
			_exitButton.onClick.RemoveListener(GotoMainMenu);
		}

		public void Resume()
		{
			States.instance.Pop();
		}

		public void Settings()
		{
			States.instance.Push<SettingsState>();
		}

        public void GotoMainMenu()
		{
            SceneManager.LoadScene("Main Menu");
            GameEvents.SwitchMap(uiInputMap);
        }
	}
}
