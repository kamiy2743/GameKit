using System.Text.RegularExpressions;

namespace GameKit.LocalStorage
{
    public sealed record LocalStorageKey
    {
        const string KeyPrefix = "LocalStorage: ";
        const string CategoryPrefix = "Category: ";
        const string ValuePrefix = "Value: ";

        public string Key { get; }
        public string? Category { get; }

        public LocalStorageKey(string value, string? category = null)
        {
            Category = category;

            Key = category is null
                ? $"{KeyPrefix}{ValuePrefix}{value}"
                : $"{KeyPrefix}{CategoryPrefix}{category} {ValuePrefix}{value}";
        }
        
        public static bool TryParse(string key, out LocalStorageKey? localStorageKey)
        {
            if (!key.StartsWith(KeyPrefix))
            {
                localStorageKey = null;
                return false;
            }
            
            var category = Regex.Match(key, $"{CategoryPrefix}(.*) {ValuePrefix}").Groups[1].Value;
            var value = Regex.Match(key, $"{ValuePrefix}(.*)").Groups[1].Value;

            localStorageKey = string.IsNullOrEmpty(category)
                ? new LocalStorageKey(value)
                : new LocalStorageKey(value, category);
            return true;
        }
    }
}