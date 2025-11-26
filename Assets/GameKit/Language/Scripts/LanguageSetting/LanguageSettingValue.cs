using GameKit.Localization;
using GameKit.LocalStorage.LocalStorageValue;
using GameKit.Setting.SettingValue;

namespace GameKit.Language.LanguageSetting
{
    public sealed record LanguageSettingValue(Locale Value)
        : IDropdownSettingValue<Locale>, IStringLocalStorageValue<LanguageSettingValue>
    {
        public Locale Value { get; } = Value;
        
        string IStringLocalStorageValue<LanguageSettingValue>.Serialize()
        {
            return Value.Code;
        }
        
        LanguageSettingValue IStringLocalStorageValue<LanguageSettingValue>.Deserialize(string value)
        {
            var locale = Locale.FromCode(value);
            return new LanguageSettingValue(locale);
        }
    }
}