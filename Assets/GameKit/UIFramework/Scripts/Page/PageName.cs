using Cysharp.Text;

namespace GameKit.UIFramework.Page
{
    //TODO valueをprivateにする
    public sealed record PageName(string Value)
    {
        // TODO Suffixでいいかも
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