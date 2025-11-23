using System.IO;
using GameKit.Asset.Editor.Prefab;
using UnityEditor;

namespace GameKit.Language.Editor
{
    static class PrefabMenu
    {
        const string HierarchyPrefabMenuRoot = "GameObject/GameKit/Language/";
        const string PackageRootPrefabPath = "Language/Prefabs/";

        [MenuItem(HierarchyPrefabMenuRoot + "LanguageSetting")]
        static void MakeLanguageSetting()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "LanguageSetting/LanguageSetting.prefab"));
        }
    }
}
