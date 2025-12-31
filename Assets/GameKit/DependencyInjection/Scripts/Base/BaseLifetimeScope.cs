using System;
using System.Reflection;
using VContainer.Unity;

namespace GameKit.DependencyInjection.Base
{
    public abstract class BaseLifetimeScope : LifetimeScope
    {
        protected abstract Type GetParentType();

        protected virtual void OnValidate()
        {
            var createMethod = typeof(ParentReference).GetMethod(
                nameof(ParentReference.Create),
                BindingFlags.Public | BindingFlags.Static
            );
            parentReference = (ParentReference)createMethod!
                .MakeGenericMethod(GetParentType())
                .Invoke(null, null);
        }
    }
}
