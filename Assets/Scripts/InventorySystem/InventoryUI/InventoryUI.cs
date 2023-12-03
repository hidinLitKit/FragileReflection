using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.Security.AccessControl;

namespace FragileReflection
{
    [System.Serializable]
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
        //С помощью этого скрипта мы перемещаемся внутри инвентаря и взаимодействуем с предметами
        [Header("Поля инвентаря")]
        [SerializeField] private InventoryObject inventory; //Ссылка на инвентарь
        [SerializeField]private ItemDatabaseObject _database; //Датабаза всех предметов в игре - доступ к предмету по ID - содержит имя, описание, картинку
        [SerializeField] private List<ItemUI> _inventorySlots; //отображение инвентаря UI

        [Header("UI")]
        [SerializeField] private GameObject _highlight;
        [SerializeField] private TextMeshProUGUI _itemDescription;
        [SerializeField] private GameObject _inventoryInterface;

        [Header("Рабочие поля")]
        private InventorySlot _currentItem; //текущий выбранный предмет
        private int _currentIndex;
        private int _currentStartIndex;

        private void Awake()
        {
            _database = inventory.database;
            UpdateObjectPosition(_highlight, 0);
            CreateSlots();
            UpdateSlots();
            ShowInventory(false);

        }
        private void OnEnable()
        {
            GameEvents.onInventoryUI += ShowInventory;
            GameEvents.onSuccesUse += InventoryUpdate;
            GameEvents.onWeaponReload += UpdateSlots;
            GameEvents.onPickItem += UpdateSlots;
        }
        private void OnDisable()
        {
            GameEvents.onInventoryUI -= ShowInventory;
            GameEvents.onSuccesUse -= InventoryUpdate;
            GameEvents.onWeaponReload -= UpdateSlots;
            GameEvents.onPickItem -= UpdateSlots;
        }
        private void ShowInventory(bool show)
        {
            _inventoryInterface.SetActive(show);
        }
        public void CreateSlots()
        {
            _currentIndex = 0;
            _currentStartIndex = 0;
            for (int i = 0; i < _inventorySlots.Count; i++)
            {
                InventorySlot newSlot = new InventorySlot();
                if (i < inventory.Container.Items.Count)
                {
                    newSlot = inventory.Container.Items[i];
                }
                else
                {
                    newSlot = null;
                }
                _inventorySlots[i].inventorySlot = newSlot;
            }
            _currentItem = _inventorySlots[_currentIndex].inventorySlot;
        }
        private void resetIndex()
        {
            _currentIndex = 0;
            _currentStartIndex = 0;
        }
        public void UpdateSlots()
        {
            for (int i = _currentStartIndex, j = 0; j < _inventorySlots.Count; i++, j++)
            {
                if (i > inventory.Container.Items.Count - 1) _inventorySlots[j].inventorySlot = null;
                else _inventorySlots[j].inventorySlot = inventory.Container.Items[i];
            }

            //Обновляет информацию на UI 
            foreach (ItemUI _slot in _inventorySlots)
            {
                if (_slot.inventorySlot != null)
                {
                    _slot.slot.GetComponent<Image>().sprite = _database.GetItem[_slot.inventorySlot.item.ID].image;
                    _slot.slot.GetComponentInChildren<TextMeshProUGUI>().text = _database.GetItem[_slot.inventorySlot.item.ID].unique ? "" : _slot.inventorySlot.amount.ToString();
                    _slot.slot.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    
                }
                else
                {
                    _slot.slot.GetComponent<Image>().sprite = null;
                    _slot.slot.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    _slot.slot.GetComponentInChildren<TextMeshProUGUI>().text = "";

                }
            }

            _currentItem = _inventorySlots[_currentIndex].inventorySlot;

            if (_currentItem != null) _itemDescription.text = _database.GetItem[_currentItem.item.ID].name + " - " + _database.GetItem[_currentItem.item.ID].Description;
            else _itemDescription.text = "";

            UpdateObjectPosition(_highlight, _currentIndex);
        }


        //Гайд как вывести на экран любой предмет из инвентаря
        void howToDisplayItem()
        {
            GameObject slot; //это типа слот UI
            InventorySlot item = inventory.Container.Items[0]; //Мы взяли тут просто первый предмет который есть
        }

        public void useItem()
        {
            //если мы кликнули по кнопке вызывается этот метод
            _database.GetItem[ _currentItem.item.ID].Use();
        }

        private void InventoryUpdate(int num)
        {
            resetIndex();
            UpdateSlots();
        }

        public void examineItem()
        {
            _database.GetItem[_currentItem.item.ID].Examine();
        }

        public void moveRight()
        {
            if (_currentStartIndex + _currentIndex < inventory.Container.Items.Count - 1)
            {
                if (_currentIndex + 1 == _inventorySlots.Count)
                {
                    _currentIndex = _inventorySlots.Count - 1;
                    _currentStartIndex++;
                }
                else _currentIndex++;
                Debug.Log("Moved Right." + " Index item = " + _currentIndex);
                Debug.Log("Moved Right." + " Start index = " + _currentStartIndex);
            }
            else
                Debug.Log("You have reached end of inventory!");
            UpdateSlots();
        }

        public void moveLeft()
        {

            if (_currentStartIndex !=0 || _currentIndex!=0 )
            {
                if (_currentStartIndex > 0 && _currentIndex - 1 < 0)
                {
                    _currentStartIndex--;
                    _currentIndex = 0;
                }
                else _currentIndex--;
                Debug.Log("Moved Left." + " Index item = " + _currentIndex);
                Debug.Log("Moved Left." + " Start index = " + _currentStartIndex);
            }
            else
                Debug.Log("You reached beginning of inventory!");
            UpdateSlots();

        }

        private void UpdateObjectPosition(GameObject targetObject, int curIndex)
        {
            if (targetObject != null)
            {
                Transform objectTransform = targetObject.transform;
                float currentValue = _inventorySlots[curIndex].slot.transform.position.x;

                objectTransform.position = new Vector3(currentValue, objectTransform.position.y, objectTransform.position.z);
            }
            else
            {
                Debug.LogError("Object not found.");
            }
        }

    }
}
