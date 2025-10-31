using System.IO;
using GameKit.Asset.Editor.Prefab;
using UnityEditor;

namespace GameKit.UIFramework.Editor
{
    static class PrefabVariantMenu
    {
        const string MenuItemRoot = "Assets/Create/GameKit/UIFramework/";
        const string PackageRootPrefabPath = "UIFramework/Prefabs/";

        [MenuItem(MenuItemRoot + "Page")]
        static void MakePageVariant()
        {
            PrefabVariantFactory.Make(Path.Combine(PackageRootPrefabPath, "Page/Page.prefab"));
        }
        
        [MenuItem(MenuItemRoot + "Modal")]
        static void MakeModalVariant()
        {
            PrefabVariantFactory.Make(Path.Combine(PackageRootPrefabPath, "Modal/Modal.prefab"));
        }

        [MenuItem(MenuItemRoot + "Sheet")]
        static void MakeSheetVariant()
        {
            PrefabVariantFactory.Make(Path.Combine(PackageRootPrefabPath, "Sheet/Sheet.prefab"));
        }
    }
}
