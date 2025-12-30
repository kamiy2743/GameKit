using GameKit.DependencyInjection.Root;
using VContainer;

namespace GameKit.Setting
{
    public sealed class SettingLifetimeScope : BaseRootLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<SettingHolder>(Lifetime.Singleton);
            builder.Register<SettingBinder>(Lifetime.Singleton);
        }
    }
}