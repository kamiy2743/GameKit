using System;

namespace GameKit.UIFramework.UnityScreenNavigatorResource
{
    public sealed record ResourceKey(string Value)
    {
        public string Value { get; } = Value;
        
        public static ResourceKey FromGenerics<T>() where T : IUnityScreenNavigatorResource
        {
            return new ResourceKey(typeof(T).Name);
        }
        
        public static ResourceKey FromType(Type type)
        {
            if (!typeof(IUnityScreenNavigatorResource).IsAssignableFrom(type))
            {
                throw new ArgumentException($"{type.FullName}はIUnityScreenNavigatorResourceを実装していません。");
            }
            return new ResourceKey(type.Name);
        }
    }
}