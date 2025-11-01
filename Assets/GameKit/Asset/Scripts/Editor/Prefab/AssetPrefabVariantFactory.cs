using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameKit.Asset.Editor.Prefab
{
    public static class AssetPrefabVariantFactory
    {
        public static void Make(string packageRootPath)
        {
            var destinationFolder = SelectedProjectFolderPathProvider.Get();
            var sourcePrefabName = Path.GetFileName(packageRootPath);
            var assetPath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(destinationFolder, sourcePrefabName));

            var sourcePrefab = PackageRootAssetLoader.Load<GameObject>(packageRootPath);
            var instance = PrefabUtility.InstantiatePrefab(sourcePrefab) as GameObject;
            instance!.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;

            PrefabUtility.SaveAsPrefabAsset(instance, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetRenamer.StartRename(assetPath);
        }
    }
}