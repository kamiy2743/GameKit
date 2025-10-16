using System;
using System.Threading.Tasks;
using UnityScreenNavigator.Runtime.Foundation.AssetLoader;

namespace GameKit.UIFramework.AssetLoader
{
    public sealed class ScriptableObjectAssetLoader : IAssetLoader
    {
        readonly ScriptableObjectAssetLoaderMap map;
        
        int nextControlId;

        public ScriptableObjectAssetLoader(ScriptableObjectAssetLoaderMap map)
        {
            this.map = map;
        }
        
        AssetLoadHandle<T> IAssetLoader.Load<T>(string key)
        {
            var controlId = nextControlId++;

            var handle = new AssetLoadHandle<T>(controlId);
            IAssetLoadHandleSetter<T> setter = handle;
            var result = map.GetPrefab(key) as T;

            setter.SetResult(result);
            var status = result != null ? AssetLoadStatus.Success : AssetLoadStatus.Failed;
            setter.SetStatus(status);
            if (result == null)
            {
                var exception = new InvalidOperationException($"{key}が見つかりません");
                setter.SetOperationException(exception);
            }

            setter.SetPercentCompleteFunc(() => 1.0f);
            setter.SetTask(Task.FromResult(result));
            return handle;
        }

        AssetLoadHandle<T> IAssetLoader.LoadAsync<T>(string key)
        {
            return ((IAssetLoader)this).Load<T>(key);
        }

        void IAssetLoader.Release(AssetLoadHandle handle)
        {
        }
    }
}