using GameKit.DependencyInjection;
using VContainer;

namespace GameKit.UIFramework.Modal
{
    public sealed class ModalLifetimeScope : BaseLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<ModalStateHolder>(Lifetime.Singleton);
        }
    }
}