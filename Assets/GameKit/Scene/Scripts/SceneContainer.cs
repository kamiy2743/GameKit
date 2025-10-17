using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameKit.Scene
{
    public sealed class SceneContainer
    {
        public async UniTask AddAsync(SceneName sceneName, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            await SceneManager.LoadSceneAsync(sceneName.ResourceKey, LoadSceneMode.Additive);
        }
    }
}