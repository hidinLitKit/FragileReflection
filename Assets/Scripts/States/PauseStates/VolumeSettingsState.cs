using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FragileReflection
{
    public class VolumeSettingsState : GameState
    {
		[SerializeField] private Button _returnButton;
		protected override void OnEnable()
		{
			base.OnEnable();
			Time.timeScale = 0;
			GameEvents.SwitchMap(uiInputMap);

			GameEvents.onExit += Return;
			_returnButton.onClick.AddListener(Return);
		}

		protected override void OnDisable()
		{
			base.OnDisable();

			GameEvents.onExit -= Return;
			_returnButton.onClick.RemoveListener(Return);
		}
		public void Return()
		{
			States.instance.Pop();
		}
	}
}
