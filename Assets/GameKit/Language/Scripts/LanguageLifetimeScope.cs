using GameKit.DependencyInjection;
using VContainer;
using VContainer.Unity;

namespace GameKit.Language
{
    public sealed class LanguageLifetimeScope : BaseLifetimeScopeRegistration<LanguageLifetimeScope>
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LanguageSettingApplier>();
        }
    }
}