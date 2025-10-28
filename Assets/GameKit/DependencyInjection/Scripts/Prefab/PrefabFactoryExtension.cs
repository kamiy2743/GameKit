using UnityEngine;
using VContainer;

namespace GameKit.DependencyInjection.Prefab
{
    public static class PrefabFactoryExtension
    {
        public static void RegisterPrefab<T>(this IContainerBuilder builder, T prefab) where T : Component
        {
            builder.RegisterInstance(new PrefabFactory<T>(prefab));
        }
    }
}