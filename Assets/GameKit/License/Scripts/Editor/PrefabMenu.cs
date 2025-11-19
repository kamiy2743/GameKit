using System.IO;
using GameKit.Asset.Editor.Prefab;
using UnityEditor;

namespace GameKit.License.Editor
{
    static class PrefabMenu
    {
        const string AssetPrefabMenuRoot = "Assets/Create/GameKit/License/";
        const string PackageRootPrefabPath = "License/Prefabs/";

        [MenuItem(AssetPrefabMenuRoot + "LicenseModal")]
        static void MakeLicenseModal()
        {
            AssetPrefabVariantFactory.Make(Path.Combine(PackageRootPrefabPath, "LicenseModal.prefab"));
        }
    }
}
