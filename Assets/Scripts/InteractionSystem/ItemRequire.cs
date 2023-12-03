using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class ItemRequire : Interactable
    {
        [SerializeField] private KeyObject key;
        [SerializeField] private string _lockedMes;
        [SerializeField] private string _unlockedMes = "...";
        [SerializeField] private bool _DoesDissapear = false;
        private int _unactiveLayer = 9;
        private bool isLocked = true;

        public delegate void OnInteract();
        public OnInteract InteractEvent;
        public KeyObject Key => key;
        public bool IsLocked => isLocked;
        public void ChangeLockState()
        {
            isLocked = !isLocked;
            if (_DoesDissapear) LayerChange();
            InteractEvent?.Invoke();
        }

        public override string GetDescription()
        {
            if (isLocked) return _lockedMes;
            return _unlockedMes;
        }

        public override void Interact()
        {
            if (!IsLocked) return;
            if (GameInventory.instance.inventory.ContainsObject(key))
            {
                GameEvents.UseKey(key);
            }
        }
        private void LayerChange()
        {
            gameObject.layer = _unactiveLayer;
            foreach(Transform child in gameObject.transform)
            {
                child.gameObject.layer = _unactiveLayer;
            }
        }
    }
}
