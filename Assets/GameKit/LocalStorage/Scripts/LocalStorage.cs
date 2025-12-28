using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameKit.LocalStorage
{
    public sealed class LocalStorage
    {
        const string LocalStorageKeyDataKey = "LocalStorageKeyData";
        
        readonly LocalStorageValueSerializer serializer;

        public LocalStorage(LocalStorageValueSerializer serializer)
        {
            this.serializer = serializer;
            
            if (!PlayerPrefs.HasKey(LocalStorageKeyDataKey))
            {
                SetLocalStorageKeyData(new LocalStorageKeyData());
            }
        }
        
        public void Insert<T>(LocalStorageKey key, T value) where T : ILocalStorageValue
        {
            if (PlayerPrefs.HasKey(key.Key))
            {
                throw new InvalidOperationException($"{key.Key}が既に存在します。");
            }
            RegisterLocalStorageKey(key);
            PlayerPrefs.SetString(key.Key, serializer.Serialize(value));
            PlayerPrefs.Save();
        }
        
        public void InsertOrUpdate<T>(LocalStorageKey key, T value) where T : ILocalStorageValue
        {
            RegisterLocalStorageKey(key);
            PlayerPrefs.SetString(key.Key, serializer.Serialize(value));
            PlayerPrefs.Save();
        }
        
        public bool TryGet<T>(LocalStorageKey key, out T value) where T : ILocalStorageValue
        {
            if (PlayerPrefs.HasKey(key.Key))
            {
                string stringValue = PlayerPrefs.GetString(key.Key);
                value = serializer.Deserialize<T>(stringValue);
                return true;
            }
            value = default!;
            return false;
        }
        
        public IEnumerable<LocalStorageKey> GetKeys(string? category = null)
        {
            return GetLocalStorageKeyData().Keys
                .Select(key => LocalStorageKey.TryParse(key, out var localStorageKey) ? localStorageKey : null)
                .Where(key => key is not null)
                .Cast<LocalStorageKey>()
                .Where(key => category is null || key.Category!.Equals(category));
        }
        
        public void DeleteKeys(string? category = null)
        {
            var keys = GetKeys(category);
            DeregisterLocalStorageKeys(keys);
            foreach (var key in keys)
            {
                PlayerPrefs.DeleteKey(key.Key);
            }
            PlayerPrefs.Save();
        }

        static LocalStorageKeyData GetLocalStorageKeyData()
        {
            var json = PlayerPrefs.GetString(LocalStorageKeyDataKey);
            return JsonUtility.FromJson<LocalStorageKeyData>(json);
        }

        static void SetLocalStorageKeyData(LocalStorageKeyData data)
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(LocalStorageKeyDataKey, json);
            PlayerPrefs.Save();
        }

        static void RegisterLocalStorageKey(LocalStorageKey key)
        {
            var data = GetLocalStorageKeyData();
            if (data.Keys.Contains(key.Key))
            {
                return;
            }
            data.Keys.Add(key.Key);
            SetLocalStorageKeyData(data);
        }

        static void DeregisterLocalStorageKeys(IEnumerable<LocalStorageKey> keys)
        {
            var data = GetLocalStorageKeyData();
            foreach (var key in keys)
            {
                data.Keys.Remove(key.Key);
            }
            SetLocalStorageKeyData(data);
        }

        [Serializable]
        public sealed record LocalStorageKeyData
        {
            public List<string> Keys = new();
        }
    }
}