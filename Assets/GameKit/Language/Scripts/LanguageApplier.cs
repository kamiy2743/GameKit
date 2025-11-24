using System;
using GameKit.Language.LanguageSetting;
using GameKit.Localization;
using GameKit.Setting;
using R3;
using VContainer.Unity;

namespace GameKit.Language
{
    public sealed class LanguageApplier : IInitializable, IDisposable
    {
        readonly LocaleController localeController;
        readonly SettingHolder settingHolder;

        readonly CompositeDisposable disposable = new();

        public LanguageApplier(
            LocaleController localeController,
            SettingHolder settingHolder
        )
        {
            this.localeController = localeController;
            this.settingHolder = settingHolder;
        }
        
        void IInitializable.Initialize()
        {
            settingHolder.GetAsReactiveProperty<LanguageSettingProperty, LanguageSettingValue>(disposable)
                .Subscribe(x => localeController.SetLocale(x.Value))
                .AddTo(disposable);
        }
        
        void IDisposable.Dispose()
        {
            disposable.Dispose();
        }
    }
}