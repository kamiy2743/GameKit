using GameKit.DependencyInjection.Root;
using VContainer;
using VContainer.Unity;

namespace GameKit.Language
{
    public sealed class LanguageLifetimeScope : BaseRootLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LanguageApplier>();
        }
    }
}