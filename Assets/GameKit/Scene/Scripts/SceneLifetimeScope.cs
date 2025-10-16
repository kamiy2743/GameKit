using GameKit.DependencyInjection;
using VContainer;

namespace GameKit.Scene
{
    public sealed class SceneLifetimeScope : BaseLifetimeScopeRegistration<SceneLifetimeScope>
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneContainer>(Lifetime.Singleton);
        }
    }
}