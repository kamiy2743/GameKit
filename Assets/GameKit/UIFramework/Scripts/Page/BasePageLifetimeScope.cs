using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameKit.UIFramework.Page
{
    public abstract class BasePageLifetimeScope<TView, TPresenter> : LifetimeScope
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