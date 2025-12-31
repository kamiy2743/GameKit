using System;
using GameKit.DependencyInjection;

namespace GameKit.UIFramework.Modal.ConfirmModal
{
    public sealed class ConfirmModalLifetimeScope : BaseModalLifetimeScope<ConfirmModal, ConfirmModalPresenter>
    {
        protected override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }
    }
}