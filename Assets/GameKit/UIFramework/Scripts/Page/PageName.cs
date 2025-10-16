using Cysharp.Text;

namespace GameKit.UIFramework.Page
{
    public sealed record PageName(string Value)
    {
        static string ResourceKeyPrefix => "Page_";

        public string Value { get; } = Value;
        public string ResourceKey { get; } = ZString.Concat(ResourceKeyPrefix, Value);

        public static bool TryParseFromResourceKey(string value, out PageName? pageName)
        {
            if (value.StartsWith(ResourceKeyPrefix))
            {
                pageName = new PageName(value[ResourceKeyPrefix.Length..]);
                return true;
            }
            pageName = null;
            return false;
        }
    }
}