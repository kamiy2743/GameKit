using System;
using UnityEngine;
using VContainer;

namespace GameKit.DependencyInjection.Base
{
    public abstract class BaseMBLifetimeScopeRegistration : MonoBehaviour, ILifetimeScopeRegistration
    {
        public abstract Type GetParentType();
        public abstract void Configure(IContainerBuilder builder);
    }
}