using VContainer;

namespace GameKit.DependencyInjection.Base
{
    public abstract class BaseLifetimeScopeRegistration<TParent> : ILifetimeScopeRegistration
    {
        public abstract void Configure(IContainerBuilder builder);
    }
}