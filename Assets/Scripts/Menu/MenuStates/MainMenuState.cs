using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FragileReflection
{
    public class MainMenuState : GameState
    {
		[SerializeField] private Button _newgameButton;
        [SerializeField] private Button _loadSaveButton;
        [SerializeField] private Button _settingsButton;
		[SerializeField] private Button _exitButton;

		protected override void OnEnable()
		{
			base.OnEnable();
			
			UIEvents.onExit += Resume;
			_newgameButton.onClick.AddListener(Resume);
			_loadSaveButton.onClick.AddListener(Resume);
			_settingsButton.onClick.AddListener(Settings);
			_exitButton.onClick.AddListener(Quit);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			
			UIEvents.onExit -= Resume;
			_newgameButton.onClick.RemoveListener(Resume);
			_loadSaveButton.onClick.RemoveListener(Resume);
			_settingsButton.onClick.RemoveListener(Settings);
			_exitButton.onClick.RemoveListener(Quit);
		}

		public void Resume()
		{
			States.instance.Pop();
		}

		public void Settings()
		{
			States.instance.Push<MMSettingsState>();
		}

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
