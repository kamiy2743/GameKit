using System;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using GameKit.Setting.SettingComponent;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameKit.ScreenResolution.ScreenResolutionSetting
{
    public sealed class ScreenResolutionSettingLifetimeScope : BaseLifetimeScope
    {
        [SerializeField] DropdownSetting dropdownSetting;
        
        protected override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ScreenResolutionSettingPresenter>().WithParameter(dropdownSetting);
        }
    }
}