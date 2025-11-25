using System;
using UnityEngine;

namespace GameKit.LocalStorage
{
    public sealed class LocalStorage
    {
        readonly LocalStorageValueSerializer serializer;

        public LocalStorage(LocalStorageValueSerializer serializer)
        {
            this.serializer = serializer;
        }
        
        public void Insert<T>(string key, T value) where T : ILocalStorageValue
        {
            if (PlayerPrefs.HasKey(key))
            {
                throw new InvalidOperationException($"{key}が既に存在します。");
            }
            PlayerPrefs.SetString(key, serializer.Serialize(value));
            PlayerPrefs.Save();
        }
        
        public void InsertOrUpdate<T>(string key, T value) where T : ILocalStorageValue
        {
            PlayerPrefs.SetString(key, serializer.Serialize(value));
            PlayerPrefs.Save();
        }
        
        public bool TryGet<T>(string key, out T value) where T : ILocalStorageValue
        {
            if (PlayerPrefs.HasKey(key))
            {
                string stringValue = PlayerPrefs.GetString(key);
                value = serializer.Deserialize<T>(stringValue);
                return true;
            }
            value = default!;
            return false;
        }
    }
}