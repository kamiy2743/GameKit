using System;
using GameKit.ScreenResolution.ScreenResolutionSetting;
using GameKit.Setting;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace GameKit.ScreenResolution
{
    public sealed class ScreenResolutionApplier : IInitializable, IDisposable
    {
        readonly SettingHolder settingHolder;
        readonly CompositeDisposable disposable = new();

        public ScreenResolutionApplier(
            SettingHolder settingHolder
        )
        {
            this.settingHolder = settingHolder;
        }
        
        void IInitializable.Initialize()
        {
            settingHolder.GetAsReactiveProperty<ScreenResolutionSettingProperty, ScreenResolutionSettingValue>(disposable)
                .Subscribe(x =>
                {
                    if (x.Value.IsFullScreen)
                    {
                        Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, FullScreenMode.FullScreenWindow);
                    }
                    else
                    {
                        Screen.SetResolution(x.Value.Width!.Value, x.Value.Height!.Value, FullScreenMode.Windowed);
                    }
                })
                .AddTo(disposable);
        }

        void IDisposable.Dispose()
        {
            disposable.Dispose();
        }
    }
}