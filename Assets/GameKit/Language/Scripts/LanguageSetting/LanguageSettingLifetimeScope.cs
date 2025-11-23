using GameKit.DependencyInjection.Root;
using GameKit.Setting.SettingComponent;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameKit.Language.LanguageSetting
{
    public sealed class LanguageSettingLifetimeScope : BaseRootChildLifetimeScope
    {
        [SerializeField] DropdownSetting dropdownSetting;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LanguageSettingPresenter>().WithParameter(dropdownSetting);
        }
    }
}