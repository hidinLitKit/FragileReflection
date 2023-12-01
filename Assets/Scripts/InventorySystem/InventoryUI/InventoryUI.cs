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
        public InventoryObject inventory; //Ссылка на инвентарь
        [SerializeField]private ItemDatabaseObject _database; //Датабаза всех предметов в игре - доступ к предмету по ID - содержит имя, описание, картинку
        private InventorySlot _currentItem; //текущий выбранный предмет
        private int _currentIndex;
        private int _currentStartIndex;
        [SerializeField] private List<ItemUI> _inventorySlots; //отображение инвентаря UI
        //GameObject здесь - это сам объект UI

        //[Header("Настройки UI")] //здесь настраиваются позиции всех слотов инвентаря
        //public GameObject slotsPrefab; //Префаб слота инвентаря на UI 
        //public float X_START;
        //public float Y_START;
        //public int X_SPACE_BETWEEN_ITEMS;
        //public int Y_SPACE_BETWEEN_ITEMS;
        //public int NUMBER_OF_COLUMN;

        private void Awake()
        {
            _database = inventory.database;
            //CreateSlots();
            //slotsPrefab.SetActive(false);
            //UpdateObjectPosition("Highlight", 0);
            CreateSlots();
            UpdateSlots();
        }
        private void Update()
        {
            UpdateSlotImages();
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
            //_inventorySlots = new List<ItemUI>();
            ////пример метода для создания линии слотов предмета (нужно как то доделать)
            //for (int i = 0; i < inventory.Container.Items.Count; i++)
            //{
            //    var obj = Instantiate(slotsPrefab, Vector3.zero, Quaternion.identity, transform);

            //    //obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            //    // либо у меня vs code вчера устал уже, либо я что-то как-то починил
            //    // но если добавить всего один элемент, а другие закоменить, то может такой баг вылезти
            //    // также надо поставить на MoveRight точку останова
            //    ItemUI itm = new ItemUI(obj, inventory.Container.Items[i]);  
            //    _inventorySlots.Add(itm);

            //    ItemUI itm2 = new ItemUI(obj, inventory.Container.Items[i]);
            //    _inventorySlots.Add(itm2);

            //    ItemUI itm3 = new ItemUI(obj, inventory.Container.Items[i]);
            //    _inventorySlots.Add(itm3);

            //    ItemUI itm4 = new ItemUI(obj, inventory.Container.Items[i]);
            //    _inventorySlots.Add(itm4);

            //    ItemUI itm5 = new ItemUI(obj, inventory.Container.Items[i]);
            //    _inventorySlots.Add(itm5);
            //}
        }

        public void UpdateSlots()
        {
           /* _inventorySlots = new List<ItemUI>();

            for (int i = 0; i < inventory.Container.Items.Count; i++)
            {
                InventorySlot inventorySlot = inventory.Container.Items[i];
                if (i < 5)
                {
                    var obj = GameObject.Find("Slot " + (i + 1));
                    ItemUI itemUI = new ItemUI(obj, inventorySlot);
                    _inventorySlots.Add(itemUI);
                }
                else
                {
                    var obj = GameObject.Find("Slot 5");
                    ItemUI itemUI = new ItemUI(obj, inventorySlot);
                    _inventorySlots.Add(itemUI);
                }

            } */
           

            //Обновляет информацию на UI 
            foreach (ItemUI _slot in _inventorySlots)
            {
                if (_slot.inventorySlot != null)
                {
                    //Вот так в UI выводится инфа о предмете, но GetChild как то не серьезно
                    _slot.slot.GetComponent<Image>().sprite = _database.GetItem[_slot.inventorySlot.item.ID].image;
                    _slot.slot.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    //_slot.slot.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(0, 0, 0, 1);
                    //_slot.slot.GetComponentInChildren<TextMeshProUGUI>().text = _slot.inventorySlot.amount == 1 ? "" : _slot.inventorySlot.amount.ToString("n0");
                }
                else
                {
                    //это нам не подходит, у нас вообще все слоты на любой момент времени будут иметь предмет, не будет такого что там будет ID = -1
                    _slot.slot.GetComponent<Image>().sprite = null;
                    _slot.slot.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    //_slot.slot.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }

        private void UpdateSlotImages()
        {
            for(int i = _currentStartIndex, j = 0;j<_inventorySlots.Count; i++,j++)
            {
                _inventorySlots[j].inventorySlot = inventory.Container.Items[i];
            }
            UpdateSlots();




           /* foreach (ItemUI _slot in _inventorySlots)
            {
                if (index > 4)
                {
                    Image slotImage = _slot.slot.GetComponent<Image>();

                    int offset = moveRight ? 1 : -1;

                    int newSpriteIndex = (_slot.inventorySlot.ID + offset);

                    slotImage.sprite = _database.GetItem[newSpriteIndex].image;
                }
            } */
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

        //public Vector3 GetPosition(int i)
        //{
        //    return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
        //}

        public void moveRight()
        {
            // у нас при нажатии на стрелку вправо выбирается следующий предмет то есть
            //_currentItem = _inventorySlots[_currentIndex].inventorySlot;
            if (_currentStartIndex + _currentIndex < inventory.Container.Items.Count -1)
            { 
                if(_currentIndex + 1 == _inventorySlots.Count)
                {
                    _currentIndex = _inventorySlots.Count-1;
                    _currentStartIndex++;
                }
                else _currentIndex++;
                UpdateSlotImages();
                _currentItem = _inventorySlots[_currentIndex].inventorySlot;
                UpdateObjectPosition("Highlight", _currentIndex);
                

                Debug.Log("Moved Right." + " Index item = " + _currentIndex);
            }
            else
                Debug.Log("You have reached end of inventory!");

        }

        public void moveLeft()
        {
            if (_currentStartIndex > 0)
            {
                if (_currentStartIndex > 0 && _currentIndex - 1 < 0)
                {
                    _currentStartIndex--;
                    _currentIndex = 0;
                }
                else _currentIndex--;
                UpdateSlotImages();
                _currentItem = _inventorySlots[_currentIndex].inventorySlot;
                UpdateObjectPosition("Highlight", _currentIndex);
                

                Debug.Log("Moved Left." + " Index item = " + _currentIndex);
            }
            else
                Debug.Log("You reached beginning of inventory!");

        }

        private void UpdateObjectPosition(string objectName, int curIndex)
        {
            GameObject targetObject = GameObject.Find(objectName);

            if (targetObject != null)
            {
                Transform objectTransform = targetObject.transform;
                float currentValue = _inventorySlots[curIndex].slot.transform.position.x;

                objectTransform.position = new Vector3(currentValue, objectTransform.position.y, objectTransform.position.z);
            }
            else
            {
                Debug.LogError("Object with name '" + objectName + "' not found.");
            }
        }

    }
}
