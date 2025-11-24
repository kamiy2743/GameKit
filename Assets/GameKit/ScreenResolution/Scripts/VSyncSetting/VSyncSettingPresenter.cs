using System;
using GameKit.Setting;
using GameKit.Setting.SettingComponent;
using GameKit.Setting.SettingValue;
using R3;
using VContainer.Unity;

namespace GameKit.ScreenResolution.VSyncSetting
{
    public sealed class VSyncSettingPresenter : IInitializable, IDisposable
    {
        readonly BoolSetting vSyncSetting;
        readonly SettingBinder settingBinder;

        readonly CompositeDisposable disposable = new();

        public VSyncSettingPresenter(
            BoolSetting vSyncSetting,
            SettingBinder settingBinder
        )
        {
            this.vSyncSetting = vSyncSetting;
            this.settingBinder = settingBinder;
        }
        
        void IInitializable.Initialize()
        {
            settingBinder.Bind<VSyncSettingProperty, BoolSettingValue>(vSyncSetting, disposable);
        }

        void IDisposable.Dispose()
        {
            disposable.Dispose();
        }
    }
}