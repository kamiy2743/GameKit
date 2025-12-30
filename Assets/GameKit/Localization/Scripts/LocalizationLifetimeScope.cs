using GameKit.DependencyInjection.Root;
using VContainer;

namespace GameKit.Localization
{
    public sealed class LocalizationLifetimeScope : BaseRootLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<LocaleController>(Lifetime.Singleton);
        }
    }
}