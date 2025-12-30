using GameKit.DependencyInjection;
using VContainer;

namespace GameKit.Localization
{
    public sealed class LocalizationLifetimeScope : BaseLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<LocaleController>(Lifetime.Singleton);
        }
    }
}