using GameKit.Localization;
using GameKit.Setting;

namespace GameKit.Language.LanguageSetting
{
    public sealed class LanguageSettingProperty : ISettingProperty<LanguageSettingValue>
    {
        //TODO 定数にする
        public LanguageSettingValue Default => new(new Locale("日本語", "ja"));
    }
}