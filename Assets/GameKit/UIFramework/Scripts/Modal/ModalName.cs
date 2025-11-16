using System;
using Cysharp.Text;
using GameKit.UIFramework.UnityScreenNavigatorResource;

namespace GameKit.UIFramework.Modal
{
    public sealed partial record ModalName : IUnityScreenNavigatorResourceKey
    {
        static string ResourceKeySuffix => "Modal";
        
        public string ResourceKey { get; }

        public ModalName(string value)
        {
            ResourceKey = ZString.Concat(value, ResourceKeySuffix);
        }
        
        public static ModalName FromModalType(Type type)
        {
            if (!type.Name.EndsWith(ResourceKeySuffix))
            {
                throw new ArgumentException($"BaseModalを指定してください: {type.Name}");
            }

            return new ModalName(type.Name.Replace(ResourceKeySuffix, string.Empty));
        }
    }
}