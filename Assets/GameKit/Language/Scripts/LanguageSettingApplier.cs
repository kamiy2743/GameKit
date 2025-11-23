using System;
using GameKit.Language.LanguageSetting;
using GameKit.Localization;
using GameKit.Setting;
using GameKit.Setting.SettingValue;
using R3;
using VContainer.Unity;

namespace GameKit.Language
{
    public sealed class LanguageSettingApplier : IInitializable, IDisposable
    {
        readonly LocaleController localeController;
        readonly SettingHolder settingHolder;

        readonly CompositeDisposable disposable = new();

        public LanguageSettingApplier(
            LocaleController localeController,
            SettingHolder settingHolder
        )
        {
            this.localeController = localeController;
            this.settingHolder = settingHolder;
        }
        
        void IInitializable.Initialize()
        {
            settingHolder.GetAsReactiveProperty<LanguageSettingProperty, IntSettingValue>(disposable)
                //TODO
                .Subscribe(x => localeController.SetLocale(new Locale("日本語", "en")))
                .AddTo(disposable);
        }
        
        void IDisposable.Dispose()
        {
            disposable.Dispose();
        }
    }
}