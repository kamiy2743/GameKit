using System;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using GameKit.Setting.SettingComponent;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameKit.Language.LanguageSetting
{
    public sealed class LanguageSettingLifetimeScope : BaseLifetimeScope
    {
        [SerializeField] DropdownSetting dropdownSetting;
        
        protected override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LanguageSettingPresenter>().WithParameter(dropdownSetting);
        }
    }
}