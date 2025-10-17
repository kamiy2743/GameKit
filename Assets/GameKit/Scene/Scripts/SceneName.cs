using Cysharp.Text;

namespace GameKit.Scene
{
    //TODO valueをprivateにする
    public sealed record SceneName(string Value)
    {
        static string SceneSuffix => "Scene";
        
        public string Value { get; } = Value;
        public string ResourceKey { get; } = ZString.Concat(Value, SceneSuffix);
    }
}