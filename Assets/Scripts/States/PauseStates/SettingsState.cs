using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FragileReflection
{
    public class SettingsState : GameState
    {
		[SerializeField] private Button _returnButton;
        [SerializeField] private Button _graphicsButton;
		[SerializeField] private Button _volumeButton;
		[SerializeField] private Button _controllButton;

		protected override void OnEnable()
		{
			base.OnEnable();
			Time.timeScale = 0;
			GameEvents.SwitchMap(uiInputMap);

			UIEvents.onExit += Return;
			_returnButton.onClick.AddListener(Return);
			_graphicsButton.onClick.AddListener(Graphics);
			_volumeButton.onClick.AddListener(Volume);
			_controllButton.onClick.AddListener(Controll);
		}

		protected override void OnDisable()
		{
			base.OnDisable();

			UIEvents.onExit -= Return;
			_returnButton.onClick.RemoveListener(Return);
			_graphicsButton.onClick.RemoveListener(Graphics);
			_volumeButton.onClick.RemoveListener(Volume);
			_controllButton.onClick.RemoveListener(Controll);
		}

		public void Return()
		{
			States.instance.Pop();
		}

		public void Graphics()
        {
			States.instance.Push<GraphicsSettingsState>();
        }

		public void Volume()
        {
			States.instance.Push<VolumeSettingsState>();
        }

		public void Controll()
		{
			States.instance.Push<ControllSettingsState>();
		}
	}
}
