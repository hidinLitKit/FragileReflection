using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class GameCore : MonoBehaviour
    {
        private Transform player;
        private GameCore instance;

        private GameCore() { }

        public Transform GetPlayer()
        {
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player").transform;
            return player;
        }
    }
}
