using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameKit.Asset.Editor
{
    public static class PackageRootAssetLoader
    {
        public static T Load<T>(string packageRootPath) where T : Object
        {
            var packageRootFullPath = PackageRootFullPathProvider.Get(packageRootPath);
            var asset = AssetDatabase.LoadAssetAtPath<T>(packageRootFullPath);
            if (asset == null)
            {
                throw new FileNotFoundException($"{packageRootFullPath}が見つかりませんでした");
            }

            return asset;
        }
    }
}