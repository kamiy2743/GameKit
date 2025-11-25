using GameKit.DependencyInjection;
using VContainer;

namespace GameKit.LocalStorage
{
    public sealed class LocalStorageLifetimeScope : BaseLifetimeScopeRegistration<LocalStorageLifetimeScope>
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<LocalStorage>(Lifetime.Singleton);
            builder.Register<LocalStorageValueSerializer>(Lifetime.Singleton);
        }
    }
}