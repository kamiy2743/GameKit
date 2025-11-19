using GameKit.Asset.Editor;
using UnityEngine;

namespace GameKit.Localization.Editor
{
    [CreateAssetMenu(fileName = "LocalizedStringConstantsGeneratorSetting", menuName = "GameKit/Localization/LocalizedStringConstantsGeneratorSetting")]
    public sealed class LocalizedStringConstantsGeneratorSetting : ScriptableObject
    {
        [SerializeField] string namespaceName;
        [SerializeField] string outputPath;
        
        public string NamespaceName => namespaceName;
        public string OutputPath => outputPath;

        static LocalizedStringConstantsGeneratorSetting? instance;
        public static LocalizedStringConstantsGeneratorSetting? Instance
        {
            get
            {
                return instance ??= AssetFinder.Find<LocalizedStringConstantsGeneratorSetting>();
            }
        }
    }
}
