using GameKit.DependencyInjection;
using VContainer;

namespace GameKit.File
{
    public sealed class FileLifetimeScope : BaseLifetimeScopeRegistration<FileLifetimeScope>
    {
        public override void Configure(IContainerBuilder builder)
        {
            builder.Register<FileBrowser>(Lifetime.Singleton);
        }
    }
}