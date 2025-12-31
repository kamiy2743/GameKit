using System;
using GameKit.DependencyInjection;
using GameKit.UIFramework.Modal;

namespace GameKit.License.LicenseModal
{
    public sealed class LicenseModalLifetimeScope : BaseModalLifetimeScope<LicenseModal, LicenseModalPresenter>
    {
        protected override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }
    }
}
