using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class Door : Interactable
    {
        [SerializeField] Transform _tpPoint;
        [SerializeField] private AudioClip _doorClip;
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
            SFXManager.instance.PlaySound(_doorClip);
            StartCoroutine(TP());
        }
        IEnumerator TP()
        {
            _player.GetComponent<CharacterController>().enabled = false;
            yield return new WaitForSeconds(0.5f);
            _player.transform.position = _tpPoint.position;
            _player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
