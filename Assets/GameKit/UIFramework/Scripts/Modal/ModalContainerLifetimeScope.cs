using System;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameKit.UIFramework.Modal
{
    public sealed class ModalContainerLifetimeScope : BaseMBLifetimeScopeRegistration
    {
        [SerializeField] UnityScreenNavigator.Runtime.Core.Modal.ModalContainer modalContainer;

        public override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<ModalContainer>(Lifetime.Singleton).WithParameter(modalContainer);
            builder.RegisterEntryPoint<UniversalModalPopper>();
        }
    }
}