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
            var parentType = GetType();
            foreach (var registration in LifetimeScopeRegistrationGatherer.Get(parentType))
            {
                registration.Configure(builder);
            }
            foreach (var registration in registrations)
            {
                if (registration.GetParentType() != parentType)
                {
                    throw new InvalidOperationException($"{registration.GetType().FullName}には親のLifetimeScopeとして{parentType.FullName}を指定してください。");
                }
                registration.Configure(builder);
            }
        }
    }
}