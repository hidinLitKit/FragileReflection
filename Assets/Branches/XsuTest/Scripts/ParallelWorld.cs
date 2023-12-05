using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class ParallelWorld : MonoBehaviour
    {
        private bool isActive = false;
        [SerializeField] private List<Material> materials;

        private void OnEnable()
        {
            GameEvents.onParallelWorldActive += ActiveVision;
        }

        private void OnDisable()
        {
            GameEvents.onParallelWorldActive -= ActiveVision;
        }

        void ActiveVision()
        {
            isActive = !isActive;
            int val = isActive ? 1 : 0;
            Debug.Log(val);
            foreach (Material mat in materials)
            {
                mat.SetColor("_BaseColor", new Color(1, 0, 0, val));
                //mat.SetFloat("_Float", val);
            }
        }
    }
}
