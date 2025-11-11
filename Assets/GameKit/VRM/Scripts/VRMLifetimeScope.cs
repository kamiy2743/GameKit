using GameKit.DependencyInjection;
using VContainer;

namespace GameKit.VRM
{
    public sealed class VRMLifetimeScope : BaseLifetimeScopeRegistration<VRMLifetimeScope>
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<VRMLoader>(Lifetime.Singleton);
        }
    }
}