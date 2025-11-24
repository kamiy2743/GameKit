using GameKit.Localization;
using GameKit.Setting;

namespace GameKit.Language.LanguageSetting
{
    public sealed class LanguageSettingProperty : ISettingProperty<LanguageSettingValue>
    {
        public LanguageSettingValue Default => new(Locale.Japanese);
    }
}