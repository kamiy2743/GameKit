using GameKit.DependencyInjection.Root;
using VContainer;

namespace GameKit.UIFramework.Modal.ConfirmModal
{
    public sealed class ConfirmModalControllerLifetimeScope : BaseRootLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<ConfirmModalController>(Lifetime.Singleton);
        }
    }
}