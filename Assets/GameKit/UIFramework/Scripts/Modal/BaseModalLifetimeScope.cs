using GameKit.DependencyInjection.Root;
using UnityEngine;
using VContainer;

namespace GameKit.UIFramework.Modal
{
    public abstract class BaseModalLifetimeScope<TModal, TModalPresenter> : BaseRootChildLifetimeScope, IModalLifetimeScope
        where TModal : BaseModal
        where TModalPresenter : BaseModalPresenter
    {
        [SerializeField] TModal modal;
        
        IPushModalParams? pushModalParams;

        protected override void OnValidate()
        {
            base.OnValidate();
            autoRun = false;
            modal = GetComponent<TModal>();
        }
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(modal);
            builder.Register<TModalPresenter>(Lifetime.Singleton).WithParameter(pushModalParams);
            
            builder.RegisterBuildCallback(container =>
            {
                var presenter = container.Resolve<TModalPresenter>();
                presenter.SetPushModalParams(pushModalParams);
                modal.AddLifecycleEvent(presenter);
            });
        }
        
        void IModalLifetimeScope.Run(IPushModalParams? pushModalParams)
        {
            this.pushModalParams = pushModalParams;
            Build();
        }
    }
}