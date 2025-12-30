using GameKit.DependencyInjection.Root;
using VContainer;

namespace GameKit.Scene
{
    public sealed class SceneLifetimeScope : BaseRootLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneContainer>(Lifetime.Singleton);
        }
    }
}