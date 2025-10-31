using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameKit.Asset.Editor.Prefab
{
    public static class PrefabVariantFactory
    {
        public static void Make(string packageRootPath)
        {
            var sourcePrefabPath = PackageRootFullPathProvider.Get(packageRootPath);
            var sourcePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(sourcePrefabPath);
            if (sourcePrefab == null)
            {
                Debug.LogError($"{sourcePrefabPath}が見つかりませんでした");
                return;
            }

            var destinationFolder = SelectedProjectFolderPathProvider.Get();
            var sourcePrefabName = Path.GetFileName(sourcePrefabPath);
            var combinedPath = Path.Combine(destinationFolder, sourcePrefabName);
            var assetPath = AssetDatabase.GenerateUniqueAssetPath(combinedPath);

            var instance = PrefabUtility.InstantiatePrefab(sourcePrefab) as GameObject;
            if (instance == null)
            {
                Debug.LogError($"{sourcePrefabPath}のインスタンス化に失敗しました");
                return;
            }
            instance.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;

            try
            {
                var variantPrefab = PrefabUtility.SaveAsPrefabAsset(instance, assetPath);
                if (variantPrefab == null)
                {
                    Debug.LogError($"{assetPath}の作成に失敗しました");
                    return;
                }
            }
            finally
            {
                Object.DestroyImmediate(instance);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetRenamer.StartRename(assetPath);
        }
    }
}