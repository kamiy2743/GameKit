using GameKit.DependencyInjection.Root;
using UnityEngine;
using VContainer;

namespace GameKit.UIFramework.Modal
{
    public abstract class BaseModalLifetimeScope<TModal, TModalPresenter> : BaseRootChildLifetimeScope
        where TModal : BaseModal
        where TModalPresenter : BaseModalPresenter
    {
        [SerializeField] TModal modal;

        protected override void OnValidate()
        {
            base.OnValidate();
            modal = GetComponent<TModal>();
        }
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(modal);
            builder.Register<TModalPresenter>(Lifetime.Singleton);
            
            builder.RegisterBuildCallback(container =>
            {
                var presenter = container.Resolve<TModalPresenter>();
                modal.AddLifecycleEvent(presenter);
            });
        }
    }
}