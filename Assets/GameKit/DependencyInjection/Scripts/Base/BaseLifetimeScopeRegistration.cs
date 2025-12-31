using System;
using VContainer;

namespace GameKit.DependencyInjection.Base
{
    public abstract class BaseLifetimeScopeRegistration : ILifetimeScopeRegistration
    {
        public abstract Type GetParentType();
        public abstract void Configure(IContainerBuilder builder);
    }
}