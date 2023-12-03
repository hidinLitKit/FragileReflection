using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class Door : Interactable
    {
        [SerializeField] Transform _tpPoint;
        [SerializeField] string _message;
        private GameObject _player;
        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        public override string GetDescription()
        {
            return _message;
        }

        public override void Interact()
        {
            StopAllCoroutines();
            GameEvents.UIFade();
            StartCoroutine(TP());
        }
        IEnumerator TP()
        {
            yield return new WaitForSeconds(0.5f);
            _player.transform.position = _tpPoint.position;
        }
    }
}
