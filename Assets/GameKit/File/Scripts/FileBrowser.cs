using System.Threading;
using Cysharp.Threading.Tasks;
using SFB;

namespace GameKit.File
{
    public sealed class FileBrowser
    {
        public async UniTask<string> SelectFileAsync(string dialogTitle, string extension, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            
            var paths = StandaloneFileBrowser.OpenFilePanel(dialogTitle, "", extension, false);
            if (paths.Length == 0 || string.IsNullOrEmpty(paths[0]))
            {
                throw new SelectFileCanceledException();
            }
            return paths[0];
        }
    }
}