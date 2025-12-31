using System;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using VContainer;

namespace GameKit.UIFramework.Modal.ConfirmModal
{
    public sealed class ConfirmModalControllerLifetimeScope : BaseLifetimeScopeRegistration
    {
        public override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<ConfirmModalController>(Lifetime.Singleton);
        }
    }
}