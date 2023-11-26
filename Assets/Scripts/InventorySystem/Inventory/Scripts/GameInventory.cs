using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FragileReflection
{
    public class GameInventory : MonoBehaviour
    {
        [HideInInspector] public static GameInventory instance = null;
        public InventoryObject inventory;
        private void OnEnable()
        {
            GameEvents.onSuccesUse += ManageItem;
        }
        private void OnDisable()
        {
            GameEvents.onSuccesUse -= ManageItem;
        }
        void Start()
        {
            if (instance == null) instance = this;
            else if (instance == this) Destroy(gameObject);
            //DontDestroyOnLoad(gameObject);
            //�������������� �������� (���� ����� �����);
        }
        private void ManageItem(int id)
        {
            //��������� memoryleak (�� ������� ����� item, ����� ������� �� ��������� �������� item, ��������� ��� ����� � ���������)
            inventory.RemoveItem(id);
        }

    }
}
