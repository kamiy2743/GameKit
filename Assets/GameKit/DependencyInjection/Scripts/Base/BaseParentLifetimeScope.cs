using System;
using UnityEngine;
using VContainer;

namespace GameKit.DependencyInjection.Base
{
    public abstract class BaseParentLifetimeScope : BaseLifetimeScope
    {
        [SerializeField] BaseMBLifetimeScopeRegistration[] registrations;

        protected override void Configure(IContainerBuilder builder)
        {
            foreach (var registration in LifetimeScopeRegistrationGatherer.Get(GetParentType()))
            {
                registration.Configure(builder);
            }
            foreach (var registration in registrations)
            {
                if (registration.GetParentType() != GetParentType())
                {
                    throw new InvalidOperationException("親が異なるLifetimeScopeRegistrationが含まれています。");
                }
                registration.Configure(builder);
            }
        }
    }
}