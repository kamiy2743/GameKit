using System;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using VContainer;

namespace GameKit.Scene
{
    public sealed class SceneLifetimeScope : BaseLifetimeScopeRegistration
    {
        public override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneContainer>(Lifetime.Singleton);
        }
    }
}