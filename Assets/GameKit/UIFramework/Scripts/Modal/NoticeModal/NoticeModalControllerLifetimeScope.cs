using GameKit.DependencyInjection;
using VContainer;

namespace GameKit.UIFramework.Modal.NoticeModal
{
    public sealed class NoticeModalControllerLifetimeScope : BaseLifetimeScopeRegistration<NoticeModalControllerLifetimeScope>
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<NoticeModalController>(Lifetime.Singleton);
        }
    }
}