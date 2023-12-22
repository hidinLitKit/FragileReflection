using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class ItemExchange : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;
        private void Awake()
        {
            ObjectState.LayerChange(_gameObject, ObjectState.UnactiveLayer);
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
            ObjectState.LayerChange(_gameObject, ObjectState.InteractableLayer);
        }

    }
}
