using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace FragileReflection
{
    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("File Storage Config")]
        [SerializeField] private string fileName;
        [SerializeField] private bool useEncryption;
        private GameData gameData;
        private List<IDataPersistence> dataPersistenceObjects;
        private FileDataHandler dataHandler;
        public static DataPersistenceManager instance { get; private set; }

        private void Awake()
        {
            if (instance != null)
            {
                Debug.Log("Found more than a one DataPersistenceManager in the scene. Destroying newest.");
                Destroy(this.gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }
        private void Start()
        {
            Debug.Log(Application.persistentDataPath);
        }
        public void NewGame()
        {
            this.gameData = new GameData();
        }
        public void LoadGame()
        {
            //Load any saved data from Data Handler
            this.gameData = dataHandler.Load();
            //if no data - start a new game
            if (this.gameData == null)
            {
                Debug.Log("No saved data found. Start a new game.");
                return;
            }
            //push all saved data to the objects 
            foreach (IDataPersistence dataPersitenceObj in dataPersistenceObjects)
            {
                dataPersitenceObj.LoadData(gameData);
            }

        }
        public void SaveGame()
        {
            if (this.gameData == null)
            {
                Debug.Log("No save data found. Start a new game before trying to save it.");
            }
            //Pass the data to the scripts so they can update it
            foreach (IDataPersistence dataPersitenceObj in dataPersistenceObjects)
            {
                dataPersitenceObj.SaveData(gameData);
            }
            //Save data to the file using data handler
            dataHandler.Save(gameData);
        }

        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();
            return new List<IDataPersistence>(dataPersistenceObjects);
        }
        public bool HasGameData()
        {
            return instance.gameData != null;
        }
    }
}
