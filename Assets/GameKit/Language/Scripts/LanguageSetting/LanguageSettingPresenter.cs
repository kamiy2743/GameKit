using System;
using System.Collections.Generic;
using System.Linq;
using GameKit.Localization;
using GameKit.Setting;
using GameKit.Setting.SettingComponent;
using GameKit.UIComponent.Dropdown;
using R3;
using VContainer.Unity;

namespace GameKit.Language.LanguageSetting
{
    public sealed class LanguageSettingPresenter : IInitializable, IDisposable
    {
        readonly DropdownSetting dropdownSetting;
        readonly SettingBinder settingBinder;
        readonly LocaleController localeController;

        readonly CompositeDisposable disposable = new();

        public LanguageSettingPresenter(
            DropdownSetting dropdownSetting,
            SettingBinder settingBinder,
            LocaleController localeController
        )
        {
            this.dropdownSetting = dropdownSetting;
            this.settingBinder = settingBinder;
            this.localeController = localeController;
        }

        void IInitializable.Initialize()
        {
            var locales = localeController.GetLocales();
            dropdownSetting.SetUp(MakeOptionList(locales));

            settingBinder.Bind<LanguageSettingProperty, LanguageSettingValue>(
                dropdownSetting.MakeBindable<LanguageSettingValue, Locale>(
                    locales,
                    locale => new LanguageSettingValue(locale)
                ),
                disposable
            );
        }
        
        DropdownOptionList MakeOptionList(IReadOnlyList<Locale> locales)
        {
            var options = locales.Select(x => x.Name);
            return new DropdownOptionList(new ReactiveProperty<IEnumerable<string>>(options));
        }

        void IDisposable.Dispose()
        {
            disposable.Dispose();
        }
    }
}