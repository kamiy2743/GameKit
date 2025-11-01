using UnityEditor;
using UnityEngine;

namespace GameKit.Asset.Editor.Prefab
{
    public static class HierarchyPrefabFactory
    {
        const string UndoLabel = "Hierarchy Prefab Create";

        public static void Make(string packageRootPath)
        {
            var sourcePrefab = PackageRootAssetLoader.Load<GameObject>(packageRootPath);
            var instance = PrefabUtility.InstantiatePrefab(sourcePrefab, Selection.activeTransform) as GameObject;

            if (Selection.activeTransform != null)
            {
                GameObjectUtility.SetParentAndAlign(instance, Selection.activeTransform.gameObject);
            }
            Undo.RegisterCreatedObjectUndo(instance, UndoLabel);
            GameObjectUtility.EnsureUniqueNameForSibling(instance);
            Selection.activeGameObject = instance;
        }
    }
}
