using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class Monologue : NarrativeTalker
    {
        public float txtCD;
        private bool _isCD;
        private void Awake()
        {
            _isCD = false;

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
                Debug.Log("Loop");
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
    }
}
