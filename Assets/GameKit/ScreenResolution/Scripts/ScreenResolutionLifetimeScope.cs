using System;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using VContainer;
using VContainer.Unity;

namespace GameKit.ScreenResolution
{
    public sealed class ScreenResolutionLifetimeScope : BaseLifetimeScopeRegistration
    {
        public override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ScreenResolutionApplier>();
        }
    }
}