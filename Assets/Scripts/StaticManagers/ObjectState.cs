using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public static class ObjectState
    {
        public static int DefaultLayer => _defaultLayer;
        public static int InteractableLayer => _interactableLayer;
        public static int UnactiveLayer => _unactiveLayer;
        
        private const int _defaultLayer = 0;
        private const int _interactableLayer = 6;
        private const int _unactiveLayer = 9;

        public static void LayerChange(GameObject obj, int layer)
        {
            obj.layer = layer;
            foreach (Transform child in obj.transform)
            {
                child.gameObject.layer =layer;
            }
        }



    }
}
