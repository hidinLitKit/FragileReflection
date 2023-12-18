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
		protected override void OnEnable()
		{
			base.OnEnable();
			Time.timeScale = 0;
			GameEvents.SwitchMap(uiInputMap);
			GameEvents.onExit += Resume;
			_continueButton.onClick.AddListener(Resume);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			Time.timeScale = 1;
			GameEvents.SwitchMap(playerInputMap);
			GameEvents.onExit -= Resume;
			_continueButton.onClick.RemoveListener(Resume);
		}
		public void Resume()
		{
			States.instance.Pop();
		}
		public void Settings()
		{
			
		}

		public void GotoMainMenu()
		{
			SceneManager.LoadScene("Main Menu");
		}
	}
}
