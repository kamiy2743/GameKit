using System;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using GameKit.Setting.SettingComponent;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameKit.ScreenResolution.VSyncSetting
{
    public sealed class VSyncSettingLifetimeScope : BaseLifetimeScope
    {
        [SerializeField] BoolSetting vSyncSetting;
        
        protected override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<VSyncSettingPresenter>().WithParameter(vSyncSetting);
        }
    }
}