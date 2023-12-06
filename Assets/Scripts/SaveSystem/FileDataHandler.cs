using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace FragileReflection
{
    public class FileDataHandler
    {
        private string dataDirPath = "";
        private string dataFileName = "";
        private bool useEncryption = false;
        private readonly string codeWord = "someCodeWord";
        public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
        {
            this.dataDirPath = dataDirPath;
            this.dataFileName = dataFileName;
            this.useEncryption = useEncryption;
        }
        public GameData Load()
        {
            //Path.Combine allows do avoid problems on different operating systems
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            GameData loadedData = null;
            if (File.Exists(fullPath))
            {
                try
                {
                    //Load serialized data from file
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }
                    //optionally: encrypt
                    if (useEncryption)
                    {
                        dataToLoad = EncryptDecrypt(dataToLoad);
                    }
                    //now - deserialize
                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error while loading data to a file: " + fullPath + "\n" + e);
                }
            }
            return loadedData;
        }
        public void Save(GameData data)
        {
            //Path.Combine allows do avoid problems on different operating systems
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            try
            {
                //creating the directory the file will be written to if it doesn't exist already
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                //Serialize into a JSON
                string dataToStore = JsonUtility.ToJson(data, true);

                //optionally: encrypt
                if (useEncryption)
                {
                    dataToStore = EncryptDecrypt(dataToStore);
                }

                //write serialized data to a file
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error while saving data to a file: " + fullPath + "\n" + e);
            }
        }
        //XOR Encryption
        private string EncryptDecrypt(string data)
        {
            string modifiedData = "";
            for (int i = 0; i < data.Length; i++)
            {
                modifiedData += (char)(data[i] ^ codeWord[i % codeWord.Length]);
            }
            return modifiedData;
        }
    }
}
