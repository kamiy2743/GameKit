using System.IO;
using UnityEditor;

namespace GameKit.Asset.Editor
{
    public static class SelectedProjectFolderPathProvider
    {
        public static string Get()
        {
            if (Selection.assetGUIDs != null && Selection.assetGUIDs.Length > 0)
            {
                var selectedPath = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
                if (AssetDatabase.IsValidFolder(selectedPath))
                {
                    return selectedPath;
                }

                if (!string.IsNullOrEmpty(selectedPath))
                {
                    var directory = Path.GetDirectoryName(selectedPath);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        return directory.Replace("\\", "/");
                    }
                }
            }

            var activeObject = Selection.activeObject;
            if (activeObject != null)
            {
                var activePath = AssetDatabase.GetAssetPath(activeObject);
                if (AssetDatabase.IsValidFolder(activePath))
                {
                    return activePath;
                }

                if (!string.IsNullOrEmpty(activePath))
                {
                    var directory = Path.GetDirectoryName(activePath);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        return directory.Replace("\\", "/");
                    }
                }
            }

            return "Assets";
        }
    }
}