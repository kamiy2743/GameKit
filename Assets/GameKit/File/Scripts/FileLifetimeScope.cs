using GameKit.DependencyInjection.Root;
using VContainer;

namespace GameKit.File
{
    public sealed class FileLifetimeScope : BaseRootLifetimeScopeRegistration
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<FileBrowser>(Lifetime.Singleton);
        }
    }
}