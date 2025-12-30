using VContainer.Unity;

namespace GameKit.DependencyInjection.Base
{
    public abstract class BaseChildLifetimeScope<TParent> : LifetimeScope where TParent : LifetimeScope
    {
        protected virtual void OnValidate()
        {
            parentReference = ParentReference.Create<TParent>();
        }
    }
}