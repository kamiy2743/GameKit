namespace GameKit.Localization
{
    public sealed record Locale(string Name, string Code)
    {
        public string Name { get; } = Name;
        public string Code { get; } = Code;
        
        public static Locale FromUnityLocale(UnityEngine.Localization.Locale locale)
        {
            return new Locale(locale.LocaleName, locale.Identifier.Code);
        }

        public bool Equals(Locale? other)
        {
            if (other is null)
            {
                return false;
            }
            return Code == other.Code;
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}