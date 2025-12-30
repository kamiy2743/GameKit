using GameKit.DependencyInjection.Root;
using VContainer;

namespace GameKit.Application
{
    public sealed class ApplicationLifetimeScope : BaseRootLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<ApplicationQuitter>(Lifetime.Singleton);
        }
    }
}