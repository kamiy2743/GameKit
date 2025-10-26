using GameKit.DependencyInjection.Root;
using UnityEngine;
using VContainer;

namespace GameKit.UIFramework.Page
{
    public abstract class BasePageLifetimeScope<TPage, TPresenter> : BaseRootChildLifetimeScope
        where TPage : BasePage
        where TPresenter : BasePagePresenter
    {
        [SerializeField] TPage page;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(page);
            builder.Register<TPresenter>(Lifetime.Singleton);
            
            builder.RegisterBuildCallback(container =>
            {
                var presenter = container.Resolve<TPresenter>();
                page.AddLifecycleEvent(presenter);
            });
        }
    }
}