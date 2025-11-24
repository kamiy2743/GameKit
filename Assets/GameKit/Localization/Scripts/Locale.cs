using System.Linq;

namespace GameKit.Localization
{
    public sealed partial record Locale
    {
        public string Name { get; }
        public string Code { get; }

        Locale(string name, string code)
        {
            Name = name;
            Code = code;
        }
        
        public static Locale FromCode(string code)
        {
            return Locales.First(x => x.Code == code);
        }
        
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