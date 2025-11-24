using GameKit.DependencyInjection.Root;
using GameKit.Setting.SettingComponent;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameKit.ScreenResolution.ScreenResolutionSetting
{
    public sealed class ScreenResolutionSettingLifetimeScope : BaseRootChildLifetimeScope
    {
        [SerializeField] DropdownSetting dropdownSetting;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ScreenResolutionSettingPresenter>().WithParameter(dropdownSetting);
        }
    }
}