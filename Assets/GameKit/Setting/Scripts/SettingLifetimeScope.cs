using System;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using VContainer;

namespace GameKit.Setting
{
    public sealed class SettingLifetimeScope : BaseLifetimeScopeRegistration
    {
        public override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<SettingHolder>(Lifetime.Singleton);
            builder.Register<SettingBinder>(Lifetime.Singleton);
        }
    }
}