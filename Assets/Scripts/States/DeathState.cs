using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class DeathState : GameState
    {
		protected override void OnEnable()
		{
			base.OnEnable();
			GameEvents.SwitchMap(deathInputMap);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
		}
	}
}
