using System;
using Cysharp.Text;
using GameKit.UIFramework.UnityScreenNavigatorResource;

namespace GameKit.UIFramework.Page
{
    public sealed record PageName : IUnityScreenNavigatorResourceKey
    {
        static string ResourceKeySuffix => "Page";

        public string ResourceKey { get; }

        public PageName(string value)
        {
            ResourceKey = ZString.Concat(value, ResourceKeySuffix);
        }
        
        public static PageName FromPageType(Type type)
        {
            if (!type.Name.EndsWith(ResourceKeySuffix))
            {
                throw new ArgumentException($"BasePageを指定してください: {type.Name}");
            }

            return new PageName(type.Name.Replace(ResourceKeySuffix, string.Empty));
        }
    }
}