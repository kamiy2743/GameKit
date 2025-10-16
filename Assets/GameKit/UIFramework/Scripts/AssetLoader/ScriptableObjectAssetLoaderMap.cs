using System;
using System.Collections.Generic;
using GameKit.UIFramework.Page;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Sheet;

namespace GameKit.UIFramework.AssetLoader
{
    [Serializable]
    public sealed record ScriptableObjectAssetLoaderMap
    {
        [SerializeField] List<PagePrefab> pagePrefabs;
        [SerializeField] List<ModalPrefab> modalPrefabs;
        [SerializeField] List<SheetPrefab> sheetPrefabs;
        
        public GameObject GetPrefab(string key)
        {
            if (PageName.TryParseFromResourceKey(key, out var pageName))
            {
                var page = pagePrefabs.Find(x => x.Key.Equals(pageName!.Value));
                if (page is not null)
                {
                    return page.Prefab.gameObject;
                }
            }
            //TODO

            throw new KeyNotFoundException($"{key}が見つかりません");
        }

        [Serializable]
        public sealed record PagePrefab
        {
            [SerializeField] string key;
            [SerializeField] UnityScreenNavigator.Runtime.Core.Page.Page prefab;

            public string Key => key;
            public UnityScreenNavigator.Runtime.Core.Page.Page Prefab => prefab;
        }

        [Serializable]
        public sealed record ModalPrefab
        {
            [SerializeField] string key;
            [SerializeField] Modal prefab;

            public string Key => key;
            public Modal Prefab => prefab;
        }

        [Serializable]
        public sealed record SheetPrefab
        {
            [SerializeField] string key;
            [SerializeField] Sheet prefab;

            public string Key => key;
            public Sheet Prefab => prefab;
        }
    }
}