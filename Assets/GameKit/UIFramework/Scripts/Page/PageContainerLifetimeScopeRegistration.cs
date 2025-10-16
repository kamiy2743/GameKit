using GameKit.DependencyInjection;
using UnityEngine;
using VContainer;

namespace GameKit.UIFramework.Page
{
    public sealed class PageContainerLifetimeScopeRegistration : BaseMBLifetimeScopeRegistration
    {
        [SerializeField] UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer;

        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<PageContainer>(Lifetime.Singleton).WithParameter(pageContainer);
        }
    }
}