using GameKit.DependencyInjection.Root;
using VContainer;
using VContainer.Unity;

namespace GameKit.ScreenResolution
{
    public sealed class ScreenResolutionLifetimeScope : BaseRootLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ScreenResolutionApplier>();
        }
    }
}