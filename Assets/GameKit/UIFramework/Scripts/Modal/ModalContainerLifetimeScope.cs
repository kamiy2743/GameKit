using GameKit.DependencyInjection;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameKit.UIFramework.Modal
{
    public sealed class ModalContainerLifetimeScope : BaseMBLifetimeScopeRegistration
    {
        [SerializeField] UnityScreenNavigator.Runtime.Core.Modal.ModalContainer modalContainer;

        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<ModalContainer>(Lifetime.Singleton).WithParameter(modalContainer);
            builder.RegisterEntryPoint<UniversalModalPopper>();
        }
    }
}