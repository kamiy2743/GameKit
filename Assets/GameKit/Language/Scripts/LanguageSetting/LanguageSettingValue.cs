using GameKit.Localization;
using GameKit.LocalStorage;
using GameKit.Setting.SettingValue;

namespace GameKit.Language.LanguageSetting
{
    public sealed record LanguageSettingValue(Locale Value) : IDropdownSettingValue<Locale>
    {
        public Locale Value { get; } = Value;
        
        string ILocalStorageValue.Serialize()
        {
            return Value.Code;
        }
        
        T ILocalStorageValue.Deserialize<T>(string value)
        {
            var locale = Locale.FromCode(value);
            return (T)(ILocalStorageValue)new LanguageSettingValue(locale);
        }
    }
}