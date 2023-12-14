using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;

namespace Utilities.Persistence
{
    public class SaveManager
    {
        public static void SaveData<T>(T _obj, string _table, string _key, bool _forceSave = false)
        {
            try
            {
                var _settings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                string _dataToSave = JsonConvert.SerializeObject(_obj, Formatting.Indented, _settings);

                PlayerPrefs.SetString($"{_table}_{_key}", _dataToSave);

                if (_forceSave)
                {
                    PlayerPrefs.Save();
                }

            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public static T LoadData<T>(T _defaultObj, string _table, string _key/*, bool _loadAndDestroy = false*/)
        {
            if (!PlayerPrefs.HasKey($"{_table}_{_key}"))
            {
                return _defaultObj;
            }

            string _dataLoaded = PlayerPrefs.GetString($"{_table}_{_key}");

            //if (_loadAndDestroy) DeleteData(_table, _key);
            return JsonConvert.DeserializeObject<T>(_dataLoaded);
        }

        public static bool HasKey(string _key)
        {
            return PlayerPrefs.HasKey(_key);
        }

        public static void DeleteData(string _table, string _key)
        {
            PlayerPrefs.DeleteKey($"{_table}_{_key}");
        }

        public static void UpdateData<T>(T _obj, string _table, string oldKey, string newKey)
        {
            var _settings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            string _dataToSave = JsonConvert.SerializeObject(_obj, Formatting.Indented, _settings);

            DeleteData(_table, oldKey);
            PlayerPrefs.SetString($"{_table}_{newKey}", _dataToSave);
            PlayerPrefs.Save();
        }

        #region Old Methods
        public static void SaveObject<T>(T obj, string filePath)
        {
            var folderPath = $"{Application.persistentDataPath}/{SaveKeys.GAME_SAVE_PATH}";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var binaryFormatter = new BinaryFormatter();
            var file = File.Create(filePath);
            var boardJson = JsonUtility.ToJson(obj);

            binaryFormatter.Serialize(file, boardJson);
            file.Close();
        }

        public static void LoadAndDestroyObject<T>(ref T obj, string filePath)
        {
            LoadObject(ref obj, filePath);
            DestroyObject(filePath);
        }

        public static void LoadObject<T>(ref T obj, string filePath)
        {
            if (File.Exists(filePath))
            {
                var binaryFormatter = new BinaryFormatter();
                var file = File.Open(filePath, FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)binaryFormatter.Deserialize(file), obj);
                file.Close();
            }
        }

        public static void DestroyObject(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static void DeleteSaves()
        {
            var folderPath = $"{Application.persistentDataPath}/{SaveKeys.GAME_SAVE_PATH}";

            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }

            Debug.Log("Deleting all");
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        #endregion
    }

    public static class SaveKeys
    {
        //Paths
        public const string GAME_SAVE_PATH = "game_save";

        //Keys
        public const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public const string TIME_SPAN_FORMAT = "c";
    }
}