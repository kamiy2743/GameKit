using VContainer;

namespace GameKit.DependencyInjection
{
    public interface ILifetimeScopeRegistration
    {
        void Configure(IContainerBuilder builder);
    }
}