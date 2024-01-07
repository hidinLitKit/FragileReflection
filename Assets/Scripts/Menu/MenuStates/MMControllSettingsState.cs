using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FragileReflection
{
    public class MMControllSettingsState : GameState
    {
		[SerializeField] private Button _returnButton;

		protected override void OnEnable()
		{
			base.OnEnable();
			GameEvents.SwitchMap(uiInputMap);

			UIEvents.onExit += Return;
			_returnButton.onClick.AddListener(Return);
		}

		protected override void OnDisable()
		{
			base.OnDisable();

			UIEvents.onExit -= Return;
			_returnButton.onClick.RemoveListener(Return);
		}

		public void Return()
		{
			States.instance.Pop();
		}
	}
}
