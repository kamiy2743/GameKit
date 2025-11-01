using System.IO;
using GameKit.Asset.Editor.Prefab;
using UnityEditor;

namespace GameKit.Camera.Editor
{
    static class PrefabMenu
    {
        const string HierarchyPrefabMenuRoot = "GameObject/GameKit/Camera/";
        const string PackageRootPrefabPath = "Camera/Prefabs/";

        [MenuItem(HierarchyPrefabMenuRoot + "MainCamera")]
        static void MakeMainCamera()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "MainCamera.prefab"));
        }

        [MenuItem(HierarchyPrefabMenuRoot + "FirstPersonCamera")]
        static void MakeFirstPersonCamera()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath,  "FirstPersonCamera.prefab"));    
        }

        [MenuItem(HierarchyPrefabMenuRoot + "ThirdPersonCamera")]
        static void MakeThirdPersonCamera()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "ThirdPersonCamera.prefab"));
        }
    }
}
