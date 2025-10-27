using VContainer.Unity;

namespace GameKit.DependencyInjection.Root
{
    public class BaseRootChildLifetimeScope : LifetimeScope
    {
        protected virtual void OnValidate()
        {
            parentReference = ParentReference.Create<RootLifetimeScope>();
        }
    }
}