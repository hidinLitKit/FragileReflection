using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FragileReflection
{
    public static class UIEvents 
    {
        public static event Action onExit;
        public static event Action onRight;
        public static event Action onLeft;
        public static event Action onPrimary;
        public static event Action onSecondary;

        public static void ExitPressed()
        {
            onExit?.Invoke();
        }
        public static void RightPressed()
        {
            onRight?.Invoke();
        }
        public static void LeftPressed()
        {
            onLeft?.Invoke();
        }
        public static void PrimaryPressed()
        {
            onPrimary?.Invoke();
        }
        public static void SecondaryPressed()
        {
            onSecondary?.Invoke();
        }

    }
}
