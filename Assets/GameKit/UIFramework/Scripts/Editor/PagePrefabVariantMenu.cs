using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameKit.UIFramework.Editor
{
    static class PagePrefabVariantMenu
    {
        const string PackageName = "com.kamiy2743.ui-framework";
        const string PackageRelativePrefabPath = "Prefabs/Page/Page.prefab";
        const string LegacyAssetsPrefabPath = "Assets/UIFramework/Prefabs/Page/Page.prefab";
        static readonly string SourcePrefabPath = ResolveSourcePrefabPath();
        const string DefaultFileName = "Page Variant.prefab";

        [MenuItem("Assets/Create/UIFramework/Page Variant")]
        static void CreatePageVariant()
        {
            var sourcePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(SourcePrefabPath);
            if (sourcePrefab == null)
            {
                Debug.LogError($"Page prefab が見つかりませんでした: {SourcePrefabPath}");
                return;
            }

            var destinationFolder = GetSelectedProjectFolder();
            if (string.IsNullOrEmpty(destinationFolder))
            {
                destinationFolder = "Assets";
            }

            var combinedPath = Path.Combine(destinationFolder, DefaultFileName);
            var assetPath = AssetDatabase.GenerateUniqueAssetPath(combinedPath.Replace("\\", "/"));

            var instance = PrefabUtility.InstantiatePrefab(sourcePrefab) as GameObject;
            if (instance == null)
            {
                Debug.LogError("Page prefab のインスタンス化に失敗しました。");
                return;
            }

            instance.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;

            GameObject variantAsset = null;
            try
            {
                variantAsset = PrefabUtility.SaveAsPrefabAsset(instance, assetPath);
                if (variantAsset == null)
                {
                    Debug.LogError($"Prefab Variant の作成に失敗しました: {assetPath}");
                    return;
                }
            }
            finally
            {
                Object.DestroyImmediate(instance);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            if (variantAsset != null)
            {
                ProjectWindowUtil.ShowCreatedAsset(variantAsset);
            }
        }

        [MenuItem("Assets/Create/UIFramework/Page Variant", true)]
        static bool ValidateCreatePageVariant()
        {
            return AssetDatabase.LoadAssetAtPath<GameObject>(SourcePrefabPath) != null;
        }

        static string ResolveSourcePrefabPath()
        {
            var packagePath = Path.Combine("Packages", PackageName, PackageRelativePrefabPath).Replace("\\", "/");
            if (File.Exists(packagePath))
            {
                return packagePath;
            }

            if (File.Exists(LegacyAssetsPrefabPath))
            {
                return LegacyAssetsPrefabPath;
            }

            return packagePath;
        }

        static string GetSelectedProjectFolder()
        {
            if (Selection.assetGUIDs != null && Selection.assetGUIDs.Length > 0)
            {
                var selectedPath = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
                if (AssetDatabase.IsValidFolder(selectedPath))
                {
                    return selectedPath;
                }

                if (!string.IsNullOrEmpty(selectedPath))
                {
                    var directory = Path.GetDirectoryName(selectedPath);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        return directory.Replace("\\", "/");
                    }
                }
            }

            var activeObject = Selection.activeObject;
            if (activeObject != null)
            {
                var activePath = AssetDatabase.GetAssetPath(activeObject);
                if (AssetDatabase.IsValidFolder(activePath))
                {
                    return activePath;
                }

                if (!string.IsNullOrEmpty(activePath))
                {
                    var directory = Path.GetDirectoryName(activePath);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        return directory.Replace("\\", "/");
                    }
                }
            }

            return "Assets";
        }
    }
}
