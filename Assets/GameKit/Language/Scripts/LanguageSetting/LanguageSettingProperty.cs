using GameKit.Setting;
using GameKit.Setting.SettingValue;

namespace GameKit.Language.LanguageSetting
{
    public sealed class LanguageSettingProperty : ISettingProperty<IntSettingValue>
    {
        public IntSettingValue Default => new(0);
    }
}