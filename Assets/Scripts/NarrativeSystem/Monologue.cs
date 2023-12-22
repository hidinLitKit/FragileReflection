using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class Monologue : NarrativeTalker, IDataPersistence
    {
        [SerializeField] private float txtCD;
        [SerializeField] private string _id;
        private bool _isCD;


        private void Awake()
        {
            _isCD = false;

        }
        [ContextMenu("Generate id")]
        private void GenerateGuid()
        {
            _id = System.Guid.NewGuid().ToString();
        }
        private void OnValidate()
        {
            if (_id == string.Empty)
            {
                Debug.LogWarning("Object" + gameObject.name + "has not been initialized with save value");
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player") return;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(MonologueController());
        }
        private IEnumerator MonologueController()
        {
            StartNarration(NarrativeData.instance.monologueUI, NarrativeText, NarrativeData.instance.monologueTextField);
            Debug.Log("Started monologue");
            while (!isFinished)
            {
                //Debug.Log("Loop");
                yield return new WaitForEndOfFrame();
                if (NarrativeData.instance.monologueTextField.text == NarrativeText[textIndex] && !_isCD)
                {
                    _isCD = true;
                    StartCoroutine(textCD(txtCD));
                }

            }
            StopCoroutine(MonologueController());

        }
        private IEnumerator textCD(float sec)
        {
            yield return new WaitForSeconds(sec);
            _isCD = false;
            NextLine(NarrativeData.instance.monologueUI, NarrativeText, NarrativeData.instance.monologueTextField);
        }

        public void LoadData(GameData data)
        {
            bool _isColAct;
            if (!data.TriggersData.TryGetValue(_id, out _isColAct)) return;
            GetComponent<Collider>().enabled = _isColAct;
        }

        public void SaveData(GameData data)
        {
            bool _isColAct = GetComponent<Collider>().enabled;
            Debug.Log(gameObject + " collider active: " + _isColAct);
            if (data.TriggersData.ContainsKey(_id))
            {
                data.TriggersData.Remove(_id);
            }
            data.TriggersData.Add(_id, _isColAct);
            Debug.Log(gameObject + " saved, id and _isLocked " + _id + " " + _isColAct);
        }
    }
}
