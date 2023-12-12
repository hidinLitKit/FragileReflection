using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class savetest : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                DataPersistenceManager.instance.SaveGame();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                DataPersistenceManager.instance.LoadGame();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                DataPersistenceManager.instance.NewGame();
            }
        }
    }

}
