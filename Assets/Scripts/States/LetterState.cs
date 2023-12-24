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
			GameEvents.SwitchMap(uiInputMap);
			GameEvents.onExit += GameContinue;
			_continueButton.onClick.AddListener(GameContinue);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
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
