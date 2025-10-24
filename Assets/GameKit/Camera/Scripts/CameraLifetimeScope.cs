using GameKit.DependencyInjection;
using VContainer;

namespace GameKit.Camera
{
    public sealed class CameraLifetimeScope : BaseLifetimeScopeRegistration<CameraLifetimeScope>
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<CameraContainer>(Lifetime.Singleton);
        }
    }
}