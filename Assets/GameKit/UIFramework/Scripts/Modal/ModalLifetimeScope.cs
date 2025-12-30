using GameKit.DependencyInjection.Root;
using VContainer;

namespace GameKit.UIFramework.Modal
{
    public sealed class ModalLifetimeScope : BaseRootLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<ModalStateHolder>(Lifetime.Singleton);
        }
    }
}