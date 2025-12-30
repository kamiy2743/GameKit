using GameKit.DependencyInjection;
using VContainer;

namespace GameKit.UIFramework.Modal.ConfirmModal
{
    public sealed class ConfirmModalControllerLifetimeScope : BaseLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<ConfirmModalController>(Lifetime.Singleton);
        }
    }
}