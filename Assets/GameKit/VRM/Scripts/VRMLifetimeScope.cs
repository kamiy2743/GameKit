using GameKit.DependencyInjection.Root;
using VContainer;

namespace GameKit.VRM
{
    public sealed class VRMLifetimeScope : BaseRootLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<VRMLoader>(Lifetime.Singleton);
        }
    }
}