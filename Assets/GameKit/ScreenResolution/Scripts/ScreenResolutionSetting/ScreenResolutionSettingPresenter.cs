using System;
using System.Collections.Generic;
using System.Linq;
using GameKit.DisposableExtension;
using GameKit.Localization;
using GameKit.Setting;
using GameKit.Setting.SettingComponent;
using GameKit.UIComponent.Dropdown;
using R3;
using VContainer.Unity;

namespace GameKit.ScreenResolution.ScreenResolutionSetting
{
    public sealed class ScreenResolutionSettingPresenter : IInitializable, IDisposable
    {
        readonly DropdownSetting dropdownSetting;
        readonly SettingBinder settingBinder;
        readonly LocaleController localeController;

        readonly CompositeDisposable disposable = new();

        public ScreenResolutionSettingPresenter(
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
            var screenResolutions = ScreenResolution.Values;
            dropdownSetting.SetUp(MakeOptionList(screenResolutions));

            settingBinder.Bind<ScreenResolutionSettingProperty, ScreenResolutionSettingValue>(
                dropdownSetting.MakeBindable<ScreenResolutionSettingValue, ScreenResolution>(
                    screenResolutions,
                    screenResolution => new ScreenResolutionSettingValue(screenResolution)
                ),
                disposable
            );
        }
        
        DropdownOptionList MakeOptionList(IReadOnlyList<ScreenResolution> screenResolutions)
        {
            var options = localeController.OnLocaleChange()
                .Select(_ => ToOptions(screenResolutions))
                .ToReadOnlyReactiveProperty(ToOptions(screenResolutions))
                .RegisterAndReturn(disposable);

            return new DropdownOptionList(options);

            List<string> ToOptions(IReadOnlyList<ScreenResolution> values)
            {
                return values.Select(x => x.ToString()).ToList();
            }
        }

        void IDisposable.Dispose()
        {
            disposable.Dispose();
        }
    }
}