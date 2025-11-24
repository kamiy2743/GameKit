using System.IO;
using GameKit.Asset.Editor.Prefab;
using UnityEditor;

namespace GameKit.ScreenResolution.Editor
{
    static class PrefabMenu
    {
        const string HierarchyPrefabMenuRoot = "GameObject/GameKit/ScreenResolution/";
        const string PackageRootPrefabPath = "ScreenResolution/Prefabs/";

        [MenuItem(HierarchyPrefabMenuRoot + "ScreenResolutionSetting")]
        static void MakeScreenResolutionSetting()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "ScreenResolutionSetting/ScreenResolutionSetting.prefab"));
        }
    }
}
