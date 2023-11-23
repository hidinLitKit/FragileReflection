using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class HighlightMove : MonoBehaviour
    {
        public float X_START;
        public float X_SPACE_BETWEEN_ITEMS;

        public void MoveToIndex(int index)
        {
            float newX = X_START + (X_SPACE_BETWEEN_ITEMS * index);
            Vector3 newPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = newPosition;
        }
    }
}
