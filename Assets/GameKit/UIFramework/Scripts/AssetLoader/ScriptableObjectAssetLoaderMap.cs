using System;
using System.Collections.Generic;
using GameKit.UIFramework.Page;
using GameKit.UIFramework.UnityScreenNavigatorResource;
using UnityEngine;

namespace GameKit.UIFramework.AssetLoader
{
    [Serializable]
    public sealed record ScriptableObjectAssetLoaderMap
    {
        [SerializeField] List<BasePage> pagePrefabs;

        readonly Dictionary<ResourceKey, GameObject> prefabs = new();

        public void SetUp()
        {
            foreach (IUnityScreenNavigatorResource pagePrefab in pagePrefabs)
            {
                prefabs.Add(pagePrefab.GetResourceKey(), pagePrefab.GetResource());
            }
        }
        
        public GameObject GetPrefab(string key)
        {
            var resourceKey = new ResourceKey(key);
            if (prefabs.TryGetValue(resourceKey, out var prefab))
            {
                return prefab;
            }

            throw new KeyNotFoundException($"{key}が見つかりません");
        }
    }
}