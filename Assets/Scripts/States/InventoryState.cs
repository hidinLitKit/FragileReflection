using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class InventoryState : GameState
    {
		protected override void OnEnable()
		{
			base.OnEnable();
			GameEvents.SwitchMap(uiInputMap);
			UIEvents.onExit += GameContinue;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			Time.timeScale = 1;
			GameEvents.SwitchMap(playerInputMap);
			UIEvents.onExit -= GameContinue;
		}
		public void GameContinue()
		{
			States.instance.Pop();
		}
	}
}
