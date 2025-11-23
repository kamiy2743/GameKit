using System;
using GameKit.Setting;
using GameKit.Setting.SettingComponent;
using R3;
using VContainer.Unity;

namespace GameKit.Language.LanguageSetting
{
    public sealed class LanguageSettingPresenter : IInitializable, IDisposable
    {
        readonly DropdownSetting languageSetting;
        readonly SettingBinder settingBinder;
        readonly LanguageDropdownOptionFactory languageDropdownOptionFactory;

        readonly CompositeDisposable disposable = new();

        public LanguageSettingPresenter(
            DropdownSetting languageSetting,
            SettingBinder settingBinder,
            LanguageDropdownOptionFactory languageDropdownOptionFactory
        )
        {
            this.languageSetting = languageSetting;
            this.settingBinder = settingBinder;
            this.languageDropdownOptionFactory = languageDropdownOptionFactory;
        }

        void IInitializable.Initialize()
        {
            languageSetting.SetUp(languageDropdownOptionFactory.Make());
            settingBinder.Bind<LanguageSettingProperty, LanguageSettingValue>(languageSetting, disposable);
        }

        void IDisposable.Dispose()
        {
            disposable.Dispose();
        }
    }
}