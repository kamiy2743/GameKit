using VContainer;

namespace GameKit.DependencyInjection.Base
{
    public interface ILifetimeScopeRegistration
    {
        void Configure(IContainerBuilder builder);
    }
}