using UnityEngine;
using VContainer;

namespace GameKit.DependencyInjection
{
    public abstract class BaseMBLifetimeScopeRegistration : MonoBehaviour, ILifetimeScopeRegistration
    {
        public abstract void Configure(IContainerBuilder builder);
    }
}