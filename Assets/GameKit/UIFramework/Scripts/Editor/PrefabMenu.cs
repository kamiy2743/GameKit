using System.IO;
using GameKit.Asset.Editor.Prefab;
using UnityEditor;

namespace GameKit.UIFramework.Editor
{
    static class PrefabMenu
    {
        const string AssetPrefabMenuRoot = "Assets/Create/GameKit/UIFramework/";
        const string HierarchyPrefabMenuRoot = "GameObject/GameKit/UIFramework/";
        const string PackageRootPrefabPath = "UIFramework/Prefabs/";

        [MenuItem(AssetPrefabMenuRoot + "Page")]
        static void MakePageVariant()
        {
            AssetPrefabVariantFactory.Make(Path.Combine(PackageRootPrefabPath, "Page/Page.prefab"));
        }
        
        [MenuItem(AssetPrefabMenuRoot + "Page/PageGroup")]
        static void MakePageGroupVariant()
        {
            AssetPrefabVariantFactory.Make(Path.Combine(PackageRootPrefabPath, "Page/PageGroup/PageGroup.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "RootPageContainer")]
        static void MakeRootPageContainer()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Page/RootPageContainer.prefab"));
        }
        
        [MenuItem(AssetPrefabMenuRoot + "Modal")]
        static void MakeModalVariant()
        {
            AssetPrefabVariantFactory.Make(Path.Combine(PackageRootPrefabPath, "Modal/Modal.prefab"));
        }

        [MenuItem(AssetPrefabMenuRoot + "Sheet")]
        static void MakeSheetVariant()
        {
            AssetPrefabVariantFactory.Make(Path.Combine(PackageRootPrefabPath, "Sheet/Sheet.prefab"));
        }
    }
}
