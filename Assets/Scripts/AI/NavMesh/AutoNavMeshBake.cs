using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

namespace FragileReflection
{
    public class AutoNavMeshBake : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface[] surfaces;

        private void Awake()
        {
            foreach(var surface in surfaces)
            {
                surface.BuildNavMesh();
            }
        }
    }
}
