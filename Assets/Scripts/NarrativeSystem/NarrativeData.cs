using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FragileReflection
{
    [DefaultExecutionOrder(-1)]
    public class NarrativeData : MonoBehaviour
    {
        [HideInInspector] public static NarrativeData instance = null;
        //[SerializeField] private _monologueUI;
        // public monologueUI => _monologueUI; //это просто геттер
        [Header("Монолог")]
        public GameObject monologueUI;
        public TextMeshProUGUI monologueTextField;
        [Header("Записки")]
        public GameObject letterUI;
        public Image letterBackGround;
        public TextMeshProUGUI letterTextField;
        public TextMeshProUGUI letterPageCount;
        public Button letterClose;
        public Button letterNext;
        public Button letterPrev;

        void Awake ()
        {
            if (instance == null) instance = this;
            else if (instance == this) Destroy(gameObject);
            //DontDestroyOnLoad(gameObject);
            //Инициализируем менеджер (если будет нужен);
        }
    }
}
