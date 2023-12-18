using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class PlayerState : GameState
    {
        private void Awake()
        {
			GameEvents.SwitchMap(playerInputMap);
        }
        private void OnEnable()
		{
			base.OnEnable();
			GameEvents.SwitchMap(playerInputMap);
		}

		public void GotoPause()
		{
			States.instance.Push<PauseState>();
		}
	}
}
