using Cysharp.Text;

namespace GameKit.Scene
{
    //TODO valueをprivateにする
    public sealed record SceneName(string Value)
    {
        static string ResourceKeySuffix => "Scene";
        
        public string Value { get; } = Value;
        public string ResourceKey { get; } = ZString.Concat(Value, ResourceKeySuffix);
    }
}