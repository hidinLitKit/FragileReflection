using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class Monologue : NarrativeTalker
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player") return;
            GetComponent<Collider>().enabled = false;
            base.StartNarration(NarrativeData.instance.monologueUI, this.NarrativeText, NarrativeData.instance.monologueTextField);
        }

    }
}
