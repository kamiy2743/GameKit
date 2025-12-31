using GameKit.DependencyInjection.Base;
using UnityEngine;
using VContainer;

namespace GameKit.UIFramework.Page
{
    public abstract class BasePageLifetimeScope<TPage, TPagePresenter> : BaseLifetimeScope
        where TPage : BasePage
        where TPagePresenter : BasePagePresenter
    {
        [SerializeField] TPage page;

        protected override void OnValidate()
        {
            base.OnValidate();
            page = GetComponent<TPage>();
        }
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(page);
            builder.Register<TPagePresenter>(Lifetime.Singleton);
            
            builder.RegisterBuildCallback(container =>
            {
                var presenter = container.Resolve<TPagePresenter>();
                page.AddLifecycleEvent(presenter);
            });
        }
    }
}