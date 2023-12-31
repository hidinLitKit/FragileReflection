using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
	public abstract class GameState : MonoBehaviour
	{
        [Header("Enable/Disable Objects")]
		public List<GameObject> views;
		protected const string playerInputMap = "Player", uiInputMap = "UI", deathInputMap = "DeathMap";
		public AudioClip enableClip;
		public AudioClip disableClip;
		public void Enter()
		{
			gameObject.SetActive(true);
		}

		public void Exit()
		{
			gameObject.SetActive(false);
		}

		protected virtual void OnEnable()
		{
			foreach (var item in views)
			{
				SwitchUiState(true, item);
			}
			if (enableClip != null) States.instance.enableSource.clip = enableClip;
		}

		protected virtual void OnDisable()
		{
			foreach (var item in views)
			{
				SwitchUiState(false, item);
			}
			if (disableClip != null) States.instance.disableSource.clip = disableClip;
		}
		protected void SwitchUiState(bool active, GameObject item)
        {
			CanvasGroup canva;
			if(item && item.TryGetComponent<CanvasGroup>(out canva))
            {
				canva.alpha = active ? 1 : 0;
				canva.interactable = active;
				canva.blocksRaycasts = canva;
				return;
            }
			item.SetActive(active);
        }
	}
}
