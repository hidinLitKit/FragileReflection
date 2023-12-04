using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class AudioTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player") return;
            GetComponent<Collider>().enabled = false;
            GetComponent<AudioSource>().Play();
        }
    }
}
