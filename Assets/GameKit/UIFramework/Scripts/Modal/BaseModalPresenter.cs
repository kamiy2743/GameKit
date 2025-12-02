using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityScreenNavigator.Runtime.Core.Modal;

namespace GameKit.UIFramework.Modal
{
    public abstract class BaseModalPresenter : IModalLifecycleEvent
    {
        readonly CancellationTokenSource cts = new();
        
        IPushModalParams? pushModalParams;

        public void SetPushModalParams(IPushModalParams? pushModalParams)
        {
            this.pushModalParams = pushModalParams;
        }
        
        protected T GetPushModalParams<T>() where T : IPushModalParams
        {
            if (pushModalParams is not T castedParams)
            {
                throw new InvalidOperationException($"{typeof(T).Name} が設定されていません。");
            }
            return castedParams;
        }
        
        protected virtual UniTask InitializeAsync(CancellationToken ct) => UniTask.CompletedTask;
        async Task IModalLifecycleEvent.Initialize() => await InitializeAsync(cts.Token);

        protected virtual UniTask WillPushEnterAsync(CancellationToken ct) => UniTask.CompletedTask;
        async Task IModalLifecycleEvent.WillPushEnter() => await WillPushEnterAsync(cts.Token);

        protected virtual UniTask DidPushEnterAsync(CancellationToken ct) => UniTask.CompletedTask;
        async void IModalLifecycleEvent.DidPushEnter() => await DidPushEnterAsync(cts.Token).SuppressCancellationThrow();

        protected virtual UniTask WillPushExitAsync(CancellationToken ct) => UniTask.CompletedTask;
        async Task IModalLifecycleEvent.WillPushExit() => await WillPushExitAsync(cts.Token);

        protected virtual void DidPushExit() { }
        void IModalLifecycleEvent.DidPushExit() => DidPushExit();

        protected virtual UniTask WillPopEnterAsync(CancellationToken ct) => UniTask.CompletedTask;
        async Task IModalLifecycleEvent.WillPopEnter() => await WillPopEnterAsync(cts.Token);

        protected virtual void DidPopEnter() { }
        void IModalLifecycleEvent.DidPopEnter() => DidPopEnter();

        protected virtual UniTask WillPopExitAsync(CancellationToken ct) => UniTask.CompletedTask;
        async Task IModalLifecycleEvent.WillPopExit() => await WillPopExitAsync(cts.Token);

        protected virtual void DidPopExit() { }
        void IModalLifecycleEvent.DidPopExit() => DidPopExit();

        protected virtual UniTask CleanupAsync() => UniTask.CompletedTask;
        async Task IModalLifecycleEvent.Cleanup()
        {
            await CleanupAsync();
            cts.Cancel();
            cts.Dispose();
        }
    }
}
