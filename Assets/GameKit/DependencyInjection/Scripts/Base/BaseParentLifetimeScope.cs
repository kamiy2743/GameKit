using GameKit.DependencyInjection.Root;
using UnityEngine;
using VContainer;

namespace GameKit.DependencyInjection.Base
{
    public abstract class BaseParentLifetimeScope<T, TRegistration, TMBRegistration> : BaseRootChildLifetimeScope
        where TRegistration : BaseLifetimeScopeRegistration<T>
        where TMBRegistration : BaseMBLifetimeScopeRegistration<T>
    {
        [SerializeField] TMBRegistration[] registrations;

        protected override void Configure(IContainerBuilder builder)
        {
            foreach (var registration in LifetimeScopeRegistrationGatherer.Get<T, TRegistration>())
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