using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FragileReflection
{
    public class NarrativeData : MonoBehaviour
    {
        [HideInInspector] public static NarrativeData instance = null;
        [Header("�������")]
        public GameObject monologueUI;
        public TextMeshProUGUI monologueTextField;
        [Header("�������")]
        public GameObject letterUI;
        public Image letterBackGround;
        public TextMeshProUGUI letterTextField;
        public TextMeshProUGUI letterPageCount;

        void Start ()
        {
            if (instance == null) instance = this;
            else if (instance == this) Destroy(gameObject);
            //DontDestroyOnLoad(gameObject);
            //�������������� �������� (���� ����� �����);
        }
    }
}
