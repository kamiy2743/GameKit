using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Page;
using VContainer;

namespace GameKit.UIFramework.Page
{
    public abstract class BasePagePresenter : IPageLifecycleEvent
    {
        [Inject] protected readonly PageContainer pageContainer;
        
        readonly CancellationTokenSource cts = new();
        
        protected virtual UniTask InitializeAsync(CancellationToken ct) => UniTask.CompletedTask;
        async Task IPageLifecycleEvent.Initialize() => await InitializeAsync(cts.Token);

        protected virtual UniTask WillPushEnterAsync(CancellationToken ct) => UniTask.CompletedTask;
        async Task IPageLifecycleEvent.WillPushEnter() => await WillPushEnterAsync(cts.Token);

        protected virtual void DidPushEnter() { }
        void IPageLifecycleEvent.DidPushEnter() => DidPushEnter();

        protected virtual UniTask WillPushExitAsync(CancellationToken ct) => UniTask.CompletedTask;
        async Task IPageLifecycleEvent.WillPushExit() => await WillPushExitAsync(cts.Token);

        protected virtual void DidPushExit() { }
        void IPageLifecycleEvent.DidPushExit() => DidPushExit();

        protected virtual UniTask WillPopEnterAsync(CancellationToken ct) => UniTask.CompletedTask;
        async Task IPageLifecycleEvent.WillPopEnter() => await WillPopEnterAsync(cts.Token);

        protected virtual void DidPopEnter() { }
        void IPageLifecycleEvent.DidPopEnter() => DidPopEnter();

        protected virtual UniTask WillPopExitAsync(CancellationToken ct) => UniTask.CompletedTask;
        async Task IPageLifecycleEvent.WillPopExit() => await WillPopExitAsync(cts.Token);

        protected virtual void DidPopExit() { }
        void IPageLifecycleEvent.DidPopExit() => DidPopExit();

        protected virtual UniTask CleanupAsync() => UniTask.CompletedTask;
        async Task IPageLifecycleEvent.Cleanup()
        {
            await CleanupAsync();
            cts.Cancel();
            cts.Dispose();
        }
    }
}
