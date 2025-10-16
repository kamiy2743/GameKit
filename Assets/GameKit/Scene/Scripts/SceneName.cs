using Cysharp.Text;

namespace GameKit.Scene
{
    public sealed record SceneName
    {
        public string Value { get; }

        //TODO privateにする
        public SceneName(string value)
        {
            Value = ZString.Concat(value, "Scene");
        }
    }
}