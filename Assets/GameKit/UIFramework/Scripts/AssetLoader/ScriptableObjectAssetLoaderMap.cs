using System;
using System.Collections.Generic;
using GameKit.UIFramework.Modal;
using GameKit.UIFramework.Page;
using GameKit.UIFramework.UnityScreenNavigatorResource;
using UnityEngine;

namespace GameKit.UIFramework.AssetLoader
{
    [Serializable]
    public sealed record ScriptableObjectAssetLoaderMap
    {
        [SerializeField] List<BasePage> pagePrefabs;
        [SerializeField] List<BaseModal> modalPrefabs;

        readonly Dictionary<string, GameObject> prefabs = new();

        public void SetUp()
        {
            foreach (IUnityScreenNavigatorResource pagePrefab in pagePrefabs)
            {
                prefabs.Add(pagePrefab.GetResourceKey().ResourceKey, pagePrefab.GetResource());
            }
            foreach (IUnityScreenNavigatorResource modalPrefab in modalPrefabs)
            {
                prefabs.Add(modalPrefab.GetResourceKey().ResourceKey, modalPrefab.GetResource());
            }
        }
        
        public GameObject GetPrefab(string key)
        {
            if (prefabs.TryGetValue(key, out var prefab))
            {
                return prefab;
            }

            throw new KeyNotFoundException($"{key}が見つかりません");
        }
    }
}