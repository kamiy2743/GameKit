using System.IO;
using GameKit.Asset.Editor.Prefab;
using UnityEditor;

namespace GameKit.Setting.Editor
{
    static class PrefabMenu
    {
        const string AssetPrefabMenuRoot = "Assets/Create/GameKit/Setting/";
        const string HierarchyPrefabMenuRoot = "GameObject/GameKit/Setting/";
        const string PackageRootPrefabPath = "Setting/Prefabs/";
        
        [MenuItem(AssetPrefabMenuRoot + "BaseSetting")]
        static void MakeBaseSettingVariant()
        {
            AssetPrefabVariantFactory.Make(Path.Combine(PackageRootPrefabPath, "BaseSetting.prefab"));
        }

        [MenuItem(HierarchyPrefabMenuRoot + "BoolSetting")]
        static void MakeBoolSetting()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "BoolSetting.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "FloatSetting")]
        static void MakeFloatSetting()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "FloatSetting.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "FloatSliderSetting")]
        static void MakeFloatSliderSetting()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "FloatSliderSetting.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "IntSetting")]
        static void MakeIntSetting()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "IntSetting.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "IntSliderSetting")]
        static void MakeIntSliderSetting()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "IntSliderSetting.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "StringSetting")]
        static void MakeStringSetting()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "StringSetting.prefab"));
        }
    }
}
