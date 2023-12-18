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
		protected override void OnEnable()
		{
			base.OnEnable();
			Time.timeScale = 0;
			GameEvents.SwitchMap(uiInputMap);

			GameEvents.onExit += Return;
			_returnButton.onClick.AddListener(Return);
			_graphicsButton.onClick.AddListener(Graphics);
			_volumeButton.onClick.AddListener(Volume);
		}

		protected override void OnDisable()
		{
			base.OnDisable();

			GameEvents.onExit -= Return;
			_returnButton.onClick.RemoveListener(Return);
			_graphicsButton.onClick.RemoveListener(Graphics);
			_volumeButton.onClick.RemoveListener(Volume);
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
	}
}
