using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameKit.DependencyInjection.Root
{
    public sealed class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] BaseMBLifetimeScopeRegistration[] registrations;

        protected override void Configure(IContainerBuilder builder)
        {
            foreach (var registration in LifetimeScopeRegistrationGatherer.Get())
            {
                registration.Configure(builder);
            }
            foreach (var registration in registrations)
            {
                registration.Configure(builder);
            }
        }
    }
}
