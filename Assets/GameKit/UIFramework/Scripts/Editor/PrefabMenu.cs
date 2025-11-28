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
        
        [MenuItem(HierarchyPrefabMenuRoot + "RootPageContainer")]
        static void MakeRootPageContainer()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Page/RootPageContainer.prefab"));
        }
        
        [MenuItem(AssetPrefabMenuRoot + "Modal/Modal")]
        static void MakeModalVariant()
        {
            AssetPrefabVariantFactory.Make(Path.Combine(PackageRootPrefabPath, "Modal/Modal.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "RootModalContainer")]
        static void MakeRootModalContainer()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Modal/RootModalContainer.prefab"));
        }
        
        [MenuItem(AssetPrefabMenuRoot + "Modal/ConfirmModal")]
        static void MakeConfirmModalVariant()
        {
            AssetPrefabVariantFactory.Make(Path.Combine(PackageRootPrefabPath, "Modal/ConfirmModal/ConfirmModal.prefab"));
        }
        
        [MenuItem(AssetPrefabMenuRoot + "Modal/NoticeModal")]
        static void MakeNoticeModalVariant()
        {
            AssetPrefabVariantFactory.Make(Path.Combine(PackageRootPrefabPath, "Modal/NoticeModal/NoticeModal.prefab"));
        }

        [MenuItem(AssetPrefabMenuRoot + "Sheet")]
        static void MakeSheetVariant()
        {
            AssetPrefabVariantFactory.Make(Path.Combine(PackageRootPrefabPath, "Sheet/Sheet.prefab"));
        }
    }
}
