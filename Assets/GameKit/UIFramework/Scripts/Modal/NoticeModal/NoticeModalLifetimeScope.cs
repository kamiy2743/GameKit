using System;
using GameKit.DependencyInjection;

namespace GameKit.UIFramework.Modal.NoticeModal
{
    public sealed class NoticeModalLifetimeScope : BaseModalLifetimeScope<NoticeModal, NoticeModalPresenter>
    {
        protected override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }
    }
}