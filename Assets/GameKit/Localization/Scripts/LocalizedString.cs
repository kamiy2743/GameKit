using Cysharp.Text;

namespace GameKit.Localization
{
    public sealed record LocalizedString
    {
        readonly UnityEngine.Localization.LocalizedString localizedString;
        
        public LocalizedString(string group, string table, string entry)
        {
            var fullTableName = string.IsNullOrEmpty(group) ? table : ZString.Concat(group, ".", table);
            localizedString = new UnityEngine.Localization.LocalizedString(fullTableName, entry);
        }
        
        public string GetValue()
        {
            return localizedString.GetLocalizedString();
        }
        
        public static implicit operator UnityEngine.Localization.LocalizedString(LocalizedString localizedString)
        {
            return localizedString.localizedString;
        }

        public override string ToString()
        {
            return GetValue();
        }
    }
}