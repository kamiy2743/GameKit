using System;
using System.Collections.Generic;
using System.Reflection;
using VContainer;
using VContainer.Internal;
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

        protected override void OnDestroy()
        {
            InvokeBeforeDispose();
            base.OnDestroy();
        }

        void InvokeBeforeDispose()
        {
            if (Container.TryResolve<ContainerLocal<IReadOnlyList<IBeforeDisposable>>>(out var beforeDisposables))
            {
                foreach (var beforeDisposable in beforeDisposables.Value)
                {
                    beforeDisposable.BeforeDispose();
                }
            }
        }
    }
}
