using UnityEditor;
using UnityEngine;

namespace GameKit.Asset.Editor
{
    public static class AssetRenamer
    {
        const string ProjectWindowMenu = "Window/General/Project";
        const string RenameMenu = "Assets/Rename";

        public static void StartRename(string assetPath)
        {
            EditorApplication.delayCall += () =>
            {
                var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                if (asset == null)
                {
                    Debug.LogWarning($"{assetPath}が見つかりません");
                    return;
                }

                Selection.activeObject = asset;
                EditorApplication.ExecuteMenuItem(ProjectWindowMenu);
                EditorApplication.delayCall += () => EditorApplication.ExecuteMenuItem(RenameMenu);
            };
        }
    }
}