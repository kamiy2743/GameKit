using GameKit.DependencyInjection.Root;
using VContainer;

namespace GameKit.LocalStorage
{
    public sealed class LocalStorageLifetimeScope : BaseRootLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<LocalStorage>(Lifetime.Singleton);
            builder.Register<LocalStorageValueSerializer>(Lifetime.Singleton);
        }
    }
}