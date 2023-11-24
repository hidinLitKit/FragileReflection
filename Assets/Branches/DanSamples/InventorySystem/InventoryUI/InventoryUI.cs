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
        //С помощью этого скрипта мы перемещаемся внутри инвентаря и взаимодействуем с предметами
        [Header("Поля инвентаря")]
        public InventoryObject inventory; //Ссылка на инвентарь
        private ItemDatabaseObject _database; //Датабаза всех предметов в игре - доступ к предмету по ID - содержит имя, описание, картинку
        private InventorySlot _currentItem; //текущий выбранный предмет
        private int _currentIndex;
        private List<ItemUI> _inventorySlots; //отображение инвентаря UI
        //GameObject здесь - это сам объект UI

        [Header("Настройки UI")] //здесь настраиваются позиции всех слотов инвентаря
        public GameObject inventoryPrefab; //Префаб слота инвентаря на UI 
        public float X_START;
        public float Y_START;
        public int X_SPACE_BETWEEN_ITEMS;
        public int Y_SPACE_BETWEEN_ITEMS;
        public int NUMBER_OF_COLUMN;

        //[Header("чето")]
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
            //пример метода для создания линии слотов предмета (нужно как то доделать)
            for (int i = 0; i < inventory.Container.Items.Count; i++)
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

                // либо у меня vs code вчера устал уже, либо я что-то как-то починил
                // но если добавить всего один элемент, а другие закоменить, то может такой баг вылезти
                // также надо поставить на MoveRight точку останова
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
            //Обновляет информацию на UI 
            foreach(ItemUI _slot in _inventorySlots)
            {
                if (_slot.inventorySlot.ID >= 0)
                {
                    //Вот так в UI выводится инфа о предмете, но GetChild как то не серьезно
                    _slot.slot.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.inventorySlot.item.ID].image;
                    _slot.slot.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    _slot.slot.GetComponentInChildren<TextMeshProUGUI>().text = _slot.inventorySlot.amount == 1 ? "" : _slot.inventorySlot.amount.ToString("n0");
                }
                else
                {
                    //это нам не подходит, у нас вообще все слоты на любой момент времени будут иметь предмет, не будет такого что там будет ID = -1
                    _slot.slot.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                    _slot.slot.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                    _slot.slot.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }

        //Гайд как вывести на экран любой предмет из инвентаря
        void howToDisplayItem()
        {
            GameObject slot; //это типа слот UI
            InventorySlot item = inventory.Container.Items[0]; //Мы взяли тут просто первый предмет который есть
            // закомментил т.к. генерит ошибку, я не назначил slot
            //slot.GetComponent<Image>().sprite = _database.GetItem[item.item.ID].image; 
        }

        void useItem()
        {
            //если мы кликнули по кнопке вызывается этот метод
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
            // у нас при нажатии на стрелку вправо выбирается следующий предмет то есть
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
