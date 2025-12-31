using System;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameKit.UIFramework.Page
{
    public sealed class PageContainerLifetimeScope : BaseMBLifetimeScopeRegistration
    {
        [SerializeField] UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer;

        public override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }

        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<PageContainer>(Lifetime.Singleton).WithParameter(pageContainer);
            builder.RegisterEntryPoint<PageTransitioner>().AsSelf().WithParameter(pageContainer);
            builder.RegisterEntryPoint<UniversalPagePopper>();
        }
    }
}