using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class Selectable : MonoBehaviour
    {
        public void Select(){
            var renderer = GetComponent<Renderer>();
            if(renderer == null)
            {
                Debug.Log("No renderer on selectable object " + gameObject.name);
                return;
            }
            renderer.material.color = Color.yellow;
        }

        public void Deselect(){

            var renderer = GetComponent<Renderer>();
            if (renderer == null)
            {
                Debug.Log("No renderer on selectable object " + gameObject.name);
                return;
            }
            renderer.material.color = Color.white;
        }
    }
}
