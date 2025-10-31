using System.IO;

namespace GameKit.Asset.Editor
{
    public static class PackageRootFullPathProvider
    {
        const string PackageFolderName = "Packages/com.kamiy2743.game-kit";
        const string AssetsFolderName = "Assets/GameKit";
        
        public static string Get(string packageRootPath)
        {
            var packagePath = Path.Combine(PackageFolderName, packageRootPath);
            if (File.Exists(packagePath))
            {
                return packagePath;
            }

            var assetsPath = Path.Combine(AssetsFolderName, packageRootPath);
            if (File.Exists(assetsPath))
            {
                return assetsPath;
            }

            throw new FileNotFoundException($"{packageRootPath}が{PackageFolderName}フォルダにも{AssetsFolderName}フォルダにも存在しません");
        }
    }
}