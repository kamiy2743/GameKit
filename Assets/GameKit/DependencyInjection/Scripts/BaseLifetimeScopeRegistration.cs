using VContainer;

namespace GameKit.DependencyInjection
{
    public abstract class BaseLifetimeScopeRegistration : ILifetimeScopeRegistration
    {
        public abstract void Configure(IContainerBuilder builder);
    }
}