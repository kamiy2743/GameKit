using System.IO;
using GameKit.Asset.Editor.Prefab;
using UnityEditor;

namespace GameKit.DependencyInjection.Editor
{
    static class PrefabVariantMenu
    {
        const string MenuItemRoot = "Assets/Create/GameKit/DependencyInjection/";
        const string PackageRootPrefabPath = "DependencyInjection/Prefabs/";

        [MenuItem(MenuItemRoot + "RootLifetimeScope")]
        static void MakePageVariant()
        {
            PrefabVariantFactory.Make(Path.Combine(PackageRootPrefabPath, "RootLifetimeScope.prefab"));
        }
    }
}
