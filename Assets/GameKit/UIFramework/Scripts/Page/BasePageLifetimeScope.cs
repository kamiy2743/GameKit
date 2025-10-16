using GameKit.DependencyInjection.Root;
using UnityEngine;
using VContainer;

namespace GameKit.UIFramework.Page
{
    public abstract class BasePageLifetimeScope<TView, TPresenter> : BaseRootChildLifetimeScope
        where TView : BasePageView
        where TPresenter : BasePagePresenter
    {
        [SerializeField] TView view;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(view);
            builder.Register<TPresenter>(Lifetime.Singleton);
            
            builder.RegisterBuildCallback(container =>
            {
                var presenter = container.Resolve<TPresenter>();
                view.AddLifecycleEvent(presenter);
            });
        }
    }
}