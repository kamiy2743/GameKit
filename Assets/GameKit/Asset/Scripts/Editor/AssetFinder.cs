using UnityEditor;
using UnityEngine;

namespace GameKit.Asset.Editor
{
    public static class AssetFinder
    {
        public static T? Find<T>() where T : Object
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                if (string.IsNullOrEmpty(path))
                {
                    continue;
                }

                var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                if (asset != null)
                {
                    return asset;
                }
            }

            return null;
        }
    }
}
