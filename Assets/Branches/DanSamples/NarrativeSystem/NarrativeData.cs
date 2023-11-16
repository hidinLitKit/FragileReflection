using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FragileReflection
{
    public class NarrativeData : MonoBehaviour
    {
        [HideInInspector] public static NarrativeData instance = null;
        [Header("Монолог")]
        public GameObject monologueUI;
        public TextMeshProUGUI monologueTextField;
        void Start ()
        {
            if (instance == null) instance = this;
            else if (instance == this) Destroy(gameObject);
            //DontDestroyOnLoad(gameObject);
            //Инициализируем менеджер (если будет нужен);
        }
    }
}
