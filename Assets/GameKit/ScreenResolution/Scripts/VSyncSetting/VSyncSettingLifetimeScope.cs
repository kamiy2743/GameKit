using GameKit.DependencyInjection.Root;
using GameKit.Setting.SettingComponent;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameKit.ScreenResolution.VSyncSetting
{
    public sealed class VSyncSettingLifetimeScope : BaseRootChildLifetimeScope
    {
        [SerializeField] BoolSetting vSyncSetting;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<VSyncSettingPresenter>().WithParameter(vSyncSetting);
        }
    }
}