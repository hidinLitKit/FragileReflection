using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FragileReflection
{
    public class DeathUI : MonoBehaviour
    {
        public void Continue()
        {
            SceneManager.LoadScene("Main");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("Main menu");
        }
    }
}
