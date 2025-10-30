using UnityEngine;
using UnityScreenNavigator.Runtime.Foundation.AssetLoader;

namespace GameKit.UIFramework.AssetLoader
{
    [CreateAssetMenu(fileName = "ScriptableObjectAssetLoader", menuName = "GameKit/UIFramework/ScriptableObjectAssetLoader")]
    public sealed class ScriptableObjectAssetLoaderObject : AssetLoaderObject
    {
        [SerializeField] ScriptableObjectAssetLoaderMap map;
        
        IAssetLoader? loader;
        
        public override AssetLoadHandle<T> Load<T>(string key) => GetLoader().Load<T>(key);

        public override AssetLoadHandle<T> LoadAsync<T>(string key) => GetLoader().LoadAsync<T>(key);

        public override void Release(AssetLoadHandle handle) => GetLoader().Release(handle);

        IAssetLoader GetLoader()
        {
            loader ??= new ScriptableObjectAssetLoader(map);
            return loader;
        }
    }
}