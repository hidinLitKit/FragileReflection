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
			GameEvents.onExit += GameContinue;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			Time.timeScale = 1;
			GameEvents.SwitchMap(playerInputMap);
			GameEvents.onExit -= GameContinue;
		}
		public void GameContinue()
		{
			States.instance.Pop();
		}
	}
}
