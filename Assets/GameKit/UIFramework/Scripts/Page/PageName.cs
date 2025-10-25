using Cysharp.Text;

namespace GameKit.UIFramework.Page
{
    //TODO valueをprivateにする
    public sealed record PageName(string Value)
    {
        static string ResourceKeySuffix => "Page";

        public string Value { get; } = Value;
        public string ResourceKey { get; } = ZString.Concat(Value, ResourceKeySuffix);

        public static bool TryParseFromResourceKey(string value, out PageName? pageName)
        {
            if (value.EndsWith(ResourceKeySuffix))
            {
                pageName = new PageName(value[..^ResourceKeySuffix.Length]);
                return true;
            }
            pageName = null;
            return false;
        }
    }
}