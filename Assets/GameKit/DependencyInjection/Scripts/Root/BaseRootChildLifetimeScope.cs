using VContainer.Unity;

namespace GameKit.DependencyInjection.Root
{
    public class BaseRootChildLifetimeScope : LifetimeScope
    {
        void OnValidate()
        {
            parentReference = ParentReference.Create<RootLifetimeScope>();
        }
    }
}