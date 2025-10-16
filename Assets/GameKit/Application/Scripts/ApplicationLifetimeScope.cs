using GameKit.DependencyInjection;
using VContainer;

namespace GameKit.Application
{
    public sealed class ApplicationLifetimeScope : BaseLifetimeScopeRegistration<ApplicationLifetimeScope>
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<ApplicationQuitter>(Lifetime.Singleton);
        }
    }
}