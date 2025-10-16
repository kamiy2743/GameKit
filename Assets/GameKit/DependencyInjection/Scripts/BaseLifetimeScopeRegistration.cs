using VContainer;

namespace GameKit.DependencyInjection
{
    public abstract class BaseLifetimeScopeRegistration<T> : ILifetimeScopeRegistration where T : BaseLifetimeScopeRegistration<T>, new()
    {
        public abstract void Configure(IContainerBuilder builder);

        public static void Register(IContainerBuilder builder)
        {
            new T().Configure(builder);
        }
    }
}