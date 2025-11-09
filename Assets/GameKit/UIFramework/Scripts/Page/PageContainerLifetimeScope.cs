using GameKit.DependencyInjection;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameKit.UIFramework.Page
{
    public sealed class PageContainerLifetimeScope : BaseMBLifetimeScopeRegistration
    {
        [SerializeField] UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer;

        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<PageContainer>(Lifetime.Singleton).WithParameter(pageContainer);
            builder.RegisterEntryPoint<PageTransitioner>().AsSelf().WithParameter(pageContainer);
            builder.RegisterEntryPoint<UniversalPageCloser>();
        }
    }
}