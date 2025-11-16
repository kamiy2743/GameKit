using System.Threading;
using Cysharp.Threading.Tasks;
using GameKit.Localization;
using SFB;

namespace GameKit.File
{
    public sealed class FileBrowser
    {
        public async UniTask<FilePath> SelectFileAsync(LocalizedString dialogTitle, string extension, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            
            var paths = StandaloneFileBrowser.OpenFilePanel(
                dialogTitle.GetValue(),
                "",
                extension,
                false
            );
            if (paths.Length == 0 || string.IsNullOrEmpty(paths[0]))
            {
                throw new SelectFileCanceledException();
            }

            return new FilePath(paths[0]);
        }
    }
}