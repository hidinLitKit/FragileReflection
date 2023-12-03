using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class ItemExchange : MonoBehaviour
    {
        private int _interactionLayer = 6;
        private int _unactiveLayer = 9;
        [SerializeField] private GameObject _gameObject;
        private void Awake()
        {
            _gameObject.layer = _unactiveLayer;
        }
        private void OnEnable()
        {
            GetComponent<ItemRequire>().InteractEvent += ReturnItem;
        }
        private void OnDisable()
        {
            GetComponent<ItemRequire>().InteractEvent -= ReturnItem;
        }
        private void ReturnItem()
        {
            _gameObject.layer = _interactionLayer;
        }

    }
}
