using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameKit.Scene
{
    public sealed class SceneContainer
    {
        public async UniTask AddActiveSceneAsync(SceneName sceneName, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            await SceneManager.LoadSceneAsync(sceneName.ResourceKey, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName.ResourceKey));
        }
        
        public async UniTask ChangeActiveSceneAsync(SceneName sceneName, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var currentScene = SceneManager.GetActiveScene();
            await SceneManager.LoadSceneAsync(sceneName.ResourceKey, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName.ResourceKey));
            await SceneManager.UnloadSceneAsync(currentScene);
        }
    }
}