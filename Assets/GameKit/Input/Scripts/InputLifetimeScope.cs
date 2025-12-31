using System;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using VContainer;
using VContainer.Unity;

namespace GameKit.Input
{
    public sealed class InputLifetimeScope : BaseLifetimeScopeRegistration
    {
        public override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputModeContainer>(Lifetime.Singleton);
            builder.Register<InputSystemObservableFactory>(Lifetime.Singleton);
            builder.RegisterEntryPoint<CursorChanger>();
        }
    }
}