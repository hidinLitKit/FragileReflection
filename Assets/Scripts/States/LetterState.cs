using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FragileReflection
{
    public class LetterState : GameState
    {
		[SerializeField] private Button _continueButton;
		protected override void OnEnable()
		{
			base.OnEnable();
			Time.timeScale = 0;
			GameEvents.SwitchMap(uiInputMap);
			GameEvents.onExit += GameContinue;
			_continueButton.onClick.AddListener(GameContinue);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			Time.timeScale = 1;
			GameEvents.SwitchMap(playerInputMap);
			GameEvents.onExit -= GameContinue;
			_continueButton.onClick.RemoveListener(GameContinue);
		}
		public void GameContinue()
		{
			States.instance.Pop();
		}
	}
}
