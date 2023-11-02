using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class Selectable : MonoBehaviour
    {
        public void Select(){
            GetComponent<Renderer>().material.color = Color.yellow;
        }

        public void Deselect(){
            GetComponent<Renderer>().material.color = Color.gray;
        }
    }
}
