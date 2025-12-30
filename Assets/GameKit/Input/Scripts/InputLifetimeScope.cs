using GameKit.DependencyInjection.Root;
using VContainer;
using VContainer.Unity;

namespace GameKit.Input
{
    public sealed class InputLifetimeScope : BaseRootLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputModeContainer>(Lifetime.Singleton);
            builder.Register<InputSystemObservableFactory>(Lifetime.Singleton);
            builder.RegisterEntryPoint<CursorChanger>();
        }
    }
}