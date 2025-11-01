using System.IO;
using GameKit.Asset.Editor.Prefab;
using UnityEditor;

namespace GameKit.Input.Editor
{
    static class PrefabMenu
    {
        const string HierarchyPrefabMenuRoot = "GameObject/GameKit/Input/";
        const string PackageRootPrefabPath = "Input/Prefabs/";

        [MenuItem(HierarchyPrefabMenuRoot + "EventSystem")]
        static void MakeRootPageContainer()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "EventSystem.prefab"));
        }
    }
}
