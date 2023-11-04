using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XsuTest
{
    public static class GameEvents
    {
        public static event System.Action<bool> onChangePriority;

        public static void ChangePriority(bool movingCameraOn)
        {
            onChangePriority?.Invoke(movingCameraOn);
        }
    }
}
