using GameKit.DependencyInjection.Root;
using VContainer;

namespace GameKit.UIFramework.Modal.NoticeModal
{
    public sealed class NoticeModalControllerLifetimeScope : BaseRootLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<NoticeModalController>(Lifetime.Singleton);
        }
    }
}