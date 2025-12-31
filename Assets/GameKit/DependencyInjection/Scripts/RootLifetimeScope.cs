using System;
using GameKit.DependencyInjection.Base;

namespace GameKit.DependencyInjection
{
    public sealed class RootLifetimeScope : BaseParentLifetimeScope
    {
        protected override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }
    }
}
