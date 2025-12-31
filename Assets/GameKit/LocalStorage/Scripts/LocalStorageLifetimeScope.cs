using System;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using VContainer;

namespace GameKit.LocalStorage
{
    public sealed class LocalStorageLifetimeScope : BaseLifetimeScopeRegistration
    {
        public override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<LocalStorage>(Lifetime.Singleton);
            builder.Register<LocalStorageValueSerializer>(Lifetime.Singleton);
        }
    }
}