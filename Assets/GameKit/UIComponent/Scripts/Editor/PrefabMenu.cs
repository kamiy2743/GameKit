using System.IO;
using GameKit.Asset.Editor.Prefab;
using UnityEditor;

namespace GameKit.UIComponent.Editor
{
    static class PrefabMenu
    {
        const string HierarchyPrefabMenuRoot = "GameObject/GameKit/UIComponent/";
        const string PackageRootPrefabPath = "UIComponent/Prefabs/";

        [MenuItem(HierarchyPrefabMenuRoot + "Button/Button")]
        static void MakeButton()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Button/Button.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "Button/OkButton")]
        static void MakeOkButton()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Button/OkButton.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "Button/CancelButton")]
        static void MakeCancelButton()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Button/CancelButton.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "Button/BackButton")]
        static void MakeBackButton()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Button/BackButton.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "Button/SettingButton")]
        static void MakeSettingButton()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Button/SettingButton.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "Button/LicenseButton")]
        static void MakeLicenseButton()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Button/LicenseButton.prefab"));
        }
        
        [MenuItem(HierarchyPrefabMenuRoot + "Button/QuitGameButton")]
        static void MakeQuitGameButton()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Button/QuitGameButton.prefab"));
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
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Switch/Switch.prefab"));
        }

        [MenuItem(HierarchyPrefabMenuRoot + "Text")]
        static void MakeText()
        {
            HierarchyPrefabFactory.Make(Path.Combine(PackageRootPrefabPath, "Text/Text.prefab"));
        }
    }
}
