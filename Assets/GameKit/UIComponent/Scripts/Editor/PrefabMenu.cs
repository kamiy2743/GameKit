using System.IO;
using GameKit.Asset.Editor.Prefab;
using UnityEditor;

namespace GameKit.UIComponent.Editor
{
    static class PrefabMenu
    {
        const string HierarchyPrefabMenuRoot = "GameObject/GameKit/UIComponent/";
        const string PackageRootPrefabPath = "UIComponent/Prefabs/";

        [MenuItem(HierarchyPrefabMenuRoot + "Button")]
        static void MakeButton()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Button.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "InputField/FloatInputField")]
        static void MakeFloatInputField()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "InputField/FloatInputField.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "InputField/IntInputField")]
        static void MakeIntInputField()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "InputField/IntInputField.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "InputField/StringInputField")]
        static void MakeStringInputField()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "InputField/StringInputField.prefab"));
        }

        [MenuItem(HierarchyPrefabMenuRoot + "LoopScrollView/LoopVerticalScrollView")]
        static void MakeLoopVerticalScrollView()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "LoopScrollView/LoopVerticalScrollView.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "Slider/FloatSlider")]
        static void MakeFloatSlider()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Slider/FloatSlider.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "Slider/IntSlider")]
        static void MakeIntSlider()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Slider/IntSlider.prefab"));
        }

        [MenuItem(HierarchyPrefabMenuRoot + "Switch")]
        static void MakeSwitch()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Switch.prefab"));
        }

        [MenuItem(HierarchyPrefabMenuRoot + "Text")]
        static void MakeText()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Text.prefab"));
        }
    }
}
