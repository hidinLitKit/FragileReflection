using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FragileReflection
{
    public class Pause : MonoBehaviour
    {
        private void OnEnable()
        {
            GameEvents.onGamePause += Paused;
        }

        private void OnDisable()
        {
            GameEvents.onGamePause -= Paused;
        }

        private void Paused(bool status)
        {
            if (status)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
