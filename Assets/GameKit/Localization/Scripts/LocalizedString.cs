namespace GameKit.Localization
{
    public sealed record LocalizedString
    {
        readonly UnityEngine.Localization.LocalizedString localizedString;
        
        public LocalizedString(string table, string entry)
        {
            localizedString = new UnityEngine.Localization.LocalizedString(table, entry);
        }
        
        public string GetValue()
        {
            return localizedString.GetLocalizedString();
        }
        
        public static implicit operator UnityEngine.Localization.LocalizedString(LocalizedString localizedString)
        {
            return localizedString.localizedString;
        }
    }
}