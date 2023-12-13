using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FragileReflection
{
    public class GameInventory : MonoBehaviour, IDataPersistence
    {
        [HideInInspector] public static GameInventory instance = null;
        public InventoryObject inventory;
        private string objectDataString;
        private List<string> objectData;
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
            //»нициализируем менеджер (если будет нужен);
        }
        private void ManageItem(int id)
        {
            //возможный memoryleak (мы создаем копию item, чтобы удалить из инвентар€ оригинал item, сравнива€ тип копии и оригинала)
            inventory.RemoveItem(id);
        }

        public void LoadData(GameData data)
        {
            inventory.Container.Items.Clear();
            for (int i = 0; i < data.InventoryData.Count; i++)
            {
                data.InventoryData.TryGetValue(i, out objectDataString);
                objectData = objectDataString.Split(',').Select(s => s).ToList();
                Item itm = new Item(objectData[0], int.Parse(objectData[1]), bool.Parse(objectData[2]));
                inventory.SetSlot(itm, int.Parse(objectData[3]));
            }
        }
        public void SaveData(GameData data)
        {
            for(int i = 0; i<inventory.Container.Items.Count;i++)
            {
                if(data.InventoryData.ContainsKey(i)) data.InventoryData.Remove(i);
                InventorySlot invSl = inventory.Container.Items[i];
                
                objectData = new List<string>() { invSl.item.Name, (invSl.item.ID).ToString(), (invSl.item.unique).ToString(), (invSl.amount).ToString() };
                objectDataString = string.Join(",", objectData.Select(b => b.ToString()).ToArray());
                
                data.InventoryData.Add(i, objectDataString);
            }
        }
    }

}
