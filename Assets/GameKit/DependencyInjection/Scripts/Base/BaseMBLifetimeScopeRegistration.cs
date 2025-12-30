using UnityEngine;
using VContainer;

namespace GameKit.DependencyInjection.Base
{
    public abstract class BaseMBLifetimeScopeRegistration<TParent> : MonoBehaviour, ILifetimeScopeRegistration
    {
        public abstract void Configure(IContainerBuilder builder);
    }
}