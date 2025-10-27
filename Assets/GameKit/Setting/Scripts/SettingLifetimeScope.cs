using GameKit.DependencyInjection;
using VContainer;

namespace GameKit.Setting
{
    public sealed class SettingLifetimeScope : BaseLifetimeScopeRegistration<SettingLifetimeScope>
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<SettingHolder>(Lifetime.Singleton);
            builder.Register<SettingBinder>(Lifetime.Singleton);
        }
    }
}