using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;
using UnityEngine.SceneManagement;

namespace FragileReflection
{
    public class ManeMenuScritp : MonoBehaviour
    {
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene("Main");
        }
    }
}