using Newtonsoft.Json;
using UnityEngine;

namespace Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        public void Save<T>(T obj) where T : ISavable
        {
            string json = JsonConvert.SerializeObject(obj);
            PlayerPrefs.SetString(obj.SaveId, json);
        }

        public T Load<T>(string saveId) where T : ISavable
        {
            string json = PlayerPrefs.GetString(saveId);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public bool Exists(string saveId)
        {
            return PlayerPrefs.HasKey(saveId);
        }
    }
}