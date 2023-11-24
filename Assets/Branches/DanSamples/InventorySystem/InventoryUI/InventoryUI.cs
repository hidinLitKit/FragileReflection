using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

namespace FragileReflection
{   
    public class ItemUI
    {
        public GameObject slot;
        public InventorySlot inventorySlot;
        public ItemUI(GameObject obj, InventorySlot inv)
        {
            slot = obj;
            inventorySlot = inv;
        }
    }

    public class InventoryUI : MonoBehaviour
    {
        //� ������� ����� ������� �� ������������ ������ ��������� � ��������������� � ����������
        [Header("���� ���������")]
        public InventoryObject inventory; //������ �� ���������
        private ItemDatabaseObject _database; //�������� ���� ��������� � ���� - ������ � �������� �� ID - �������� ���, ��������, ��������
        private InventorySlot _currentItem; //������� ��������� �������
        private int _currentIndex;
        private List<ItemUI> _inventorySlots; //����������� ��������� UI
        //GameObject ����� - ��� ��� ������ UI

        [Header("��������� UI")] //����� ������������� ������� ���� ������ ���������
        public GameObject inventoryPrefab; //������ ����� ��������� �� UI 
        public float X_START;
        public float Y_START;
        public int X_SPACE_BETWEEN_ITEMS;
        public int Y_SPACE_BETWEEN_ITEMS;
        public int NUMBER_OF_COLUMN;

        //[Header("����")]
        //public GameObject itemPrefab;

        private void Awake()
        {
            _database = inventory.database;
            CreateSlots();
            inventoryPrefab.SetActive(false);
            UpdateObjectPosition("Highlight", 0);
        }

        public void CreateSlots()
        {
            _inventorySlots = new List<ItemUI>();
            //������ ������ ��� �������� ����� ������ �������� (����� ��� �� ��������)
            for (int i = 0; i < inventory.Container.Items.Count; i++)
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

                // ���� � ���� vs code ����� ����� ���, ���� � ���-�� ���-�� �������
                // �� ���� �������� ����� ���� �������, � ������ ����������, �� ����� ����� ��� �������
                // ����� ���� ��������� �� MoveRight ����� ��������
                ItemUI itm = new ItemUI(obj, inventory.Container.Items[i]);  
                _inventorySlots.Add(itm);

                ItemUI itm2 = new ItemUI(obj, inventory.Container.Items[i]);
                _inventorySlots.Add(itm2);

                ItemUI itm3 = new ItemUI(obj, inventory.Container.Items[i]);
                _inventorySlots.Add(itm3);

                ItemUI itm4 = new ItemUI(obj, inventory.Container.Items[i]);
                _inventorySlots.Add(itm4);
            }
        }

        public void UpdateSlots()
        {
            //��������� ���������� �� UI 
            foreach(ItemUI _slot in _inventorySlots)
            {
                if (_slot.inventorySlot.ID >= 0)
                {
                    //��� ��� � UI ��������� ���� � ��������, �� GetChild ��� �� �� ��������
                    _slot.slot.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.inventorySlot.item.ID].image;
                    _slot.slot.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    _slot.slot.GetComponentInChildren<TextMeshProUGUI>().text = _slot.inventorySlot.amount == 1 ? "" : _slot.inventorySlot.amount.ToString("n0");
                }
                else
                {
                    //��� ��� �� ��������, � ��� ������ ��� ����� �� ����� ������ ������� ����� ����� �������, �� ����� ������ ��� ��� ����� ID = -1
                    _slot.slot.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                    _slot.slot.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                    _slot.slot.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }

        //���� ��� ������� �� ����� ����� ������� �� ���������
        void howToDisplayItem()
        {
            GameObject slot; //��� ���� ���� UI
            InventorySlot item = inventory.Container.Items[0]; //�� ����� ��� ������ ������ ������� ������� ����
            // ����������� �.�. ������� ������, � �� �������� slot
            //slot.GetComponent<Image>().sprite = _database.GetItem[item.item.ID].image; 
        }

        void useItem()
        {
            //���� �� �������� �� ������ ���������� ���� �����
            _database.GetItem[ _currentItem.item.ID].Use();
        }

        void examineItem()
        {
            _database.GetItem[_currentItem.item.ID].Examine();
        }

        public Vector3 GetPosition(int i)
        {
            return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
        }

        public void moveRight()
        {
            // � ��� ��� ������� �� ������� ������ ���������� ��������� ������� �� ����
            //_currentItem = _inventorySlots[_currentIndex].inventorySlot;
            if (_currentIndex < _inventorySlots.Count - 1)
            { 
                _currentIndex++;
                _currentItem = _inventorySlots[_currentIndex].inventorySlot;
                UpdateObjectPosition("Highlight", _currentIndex);         

                Debug.Log("Moved Right." + " IndexItem = " + _currentIndex);
            }
            else
                Debug.Log("You have reached end of inventory!");

        }

        public void moveLeft()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                _currentItem = _inventorySlots[_currentIndex].inventorySlot;
                UpdateObjectPosition("Highlight", _currentIndex);

                Debug.Log("Moved Left." + " IndexItem = " + _currentIndex);
            }
            else
                Debug.Log("You reached beginning of inventory!");

        }

        public void UpdateObjectPosition(string objectName, int curIndex)
        {
            GameObject targetObject = GameObject.Find(objectName);

            string slotName = "Slot " + (curIndex + 1);
            GameObject slotObject = GameObject.Find(slotName);

            if (targetObject != null)
            {
                Transform objectTransform = targetObject.transform;
                Transform slotTransform = slotObject.transform;

                float currentValue = slotTransform.position.x;

                objectTransform.position = new Vector3(currentValue, objectTransform.position.y, objectTransform.position.z);
            }
            else
            {
                Debug.LogError("Object with name '" + objectName + "' not found.");
            }
        }


    }
}
