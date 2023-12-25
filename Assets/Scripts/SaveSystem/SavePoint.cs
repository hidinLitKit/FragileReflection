using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class SavePoint : Interactable
    {
        [SerializeField] private string _message = "сохранить игру";
        [SerializeField] private string _savedMessage = "сохранено";
        private bool _justSaved = false;
        private float _saveCD = 7f;

        public override string GetDescription()
        {
            return _justSaved ? _savedMessage : _message;
        }

        public override void Interact()
        {
            if (_justSaved) return;
            _justSaved = true;
            DataPersistenceManager.instance.SaveGame();
            AudioEvents.instance.PlaySound(AudioEvents.instance.saveAudio);
            StartCoroutine(saveCD());
        }
        private IEnumerator saveCD()
        {
            yield return new WaitForSeconds(_saveCD);
            _justSaved = false;
        }
    }
}
