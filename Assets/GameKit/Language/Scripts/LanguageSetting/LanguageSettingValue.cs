using GameKit.Localization;
using GameKit.Setting.SettingValue;

namespace GameKit.Language.LanguageSetting
{
    public sealed record LanguageSettingValue(Locale Value) : IDropdownSettingValue<Locale>
    {
        public Locale Value { get; } = Value;
    }
}