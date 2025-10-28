using UnityEngine;

namespace GameKit.DependencyInjection.Prefab
{
    public sealed class PrefabFactory<T> where T : Component
    {
        readonly T prefab;

        public PrefabFactory(T prefab)
        {
            this.prefab = prefab;
        }

        public T Make()
        {
            return Object.Instantiate(prefab);
        }
    }
}