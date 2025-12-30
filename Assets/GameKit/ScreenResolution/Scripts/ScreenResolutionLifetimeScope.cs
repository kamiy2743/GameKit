using GameKit.DependencyInjection;
using VContainer;
using VContainer.Unity;

namespace GameKit.ScreenResolution
{
    public sealed class ScreenResolutionLifetimeScope : BaseLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<ScreenResolutionApplier>();
        }
    }
}