using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameKit.UIFramework.UnityScreenNavigatorResource;
using R3;
using VContainer.Unity;

namespace GameKit.UIFramework.Page
{
    sealed class PageContainerProcessor : IPostTickable, IDisposable
    {
        readonly UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer;
        
        readonly Subject<Unit> firstPageOpened = new();
        public Observable<Unit> FirstPageOpened => firstPageOpened;
        
        readonly Subject<Unit> lastPageClosed = new();
        public Observable<Unit> LastPageClosed => lastPageClosed;
        
        readonly CancellationTokenSource processCts = new();
        
        PushRequest? currentPushRequest;
        PopRequest? currentPopRequest;
        bool isProcessing;

        public PageContainerProcessor(UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer)
        {
            this.pageContainer = pageContainer;
        }
        
        public async UniTask PushAsync(PushRequest request)
        {
            if (currentPushRequest != null)
            {
                throw new InvalidOperationException($"他のPush処理中のため、Push要求を処理できません。 {request}, {currentPushRequest}");
            }

            currentPushRequest = request;
            currentPopRequest = null;
            await UniTask.WaitUntil(() => currentPushRequest == null, cancellationToken: request.Ct);
        }
        
        public async UniTask PopAsync(PopRequest request)
        {
            if (currentPushRequest != null || currentPopRequest != null)
            {
                throw new InvalidOperationException($"他のPushまたはPop処理中のため、Pop要求を処理できません。 {request}, {currentPushRequest}, {currentPopRequest}");
            }

            currentPopRequest = request;
            await UniTask.WaitUntil(() => currentPopRequest == null, cancellationToken: request.Ct);
        }

        void IPostTickable.PostTick()
        {
            if (currentPushRequest == null && currentPopRequest == null)
            {
                return;
            }
            if (isProcessing)
            {
                return;
            }
            ProcessAsync(processCts.Token).SuppressCancellationThrow().Forget();
        }
        
        async UniTask ProcessAsync(CancellationToken processCt)
        {
            try
            {
                isProcessing = true;

                if (currentPushRequest != null)
                {
                    using var cts = CancellationTokenSource.CreateLinkedTokenSource(processCt, currentPushRequest.Ct);
                    await ProcessPushAsync(currentPushRequest, cts.Token);
                }
                if (currentPopRequest != null)
                {
                    using var cts = CancellationTokenSource.CreateLinkedTokenSource(processCt, currentPopRequest.Ct);
                    await ProcessPopAsync(currentPopRequest, cts.Token);
                }
            }
            finally
            {
                currentPushRequest = null;
                currentPopRequest = null;
                isProcessing = false;
            }
        }
        
        async UniTask ProcessPushAsync(PushRequest request, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            await pageContainer.Push(request.ResourceKey.Value, request.PlayAnimation);
            if (pageContainer.OrderedPagesIds.Count == 1)
            {
                firstPageOpened.OnNext(Unit.Default);
            }
        }
        
        async UniTask ProcessPopAsync(PopRequest request, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var beforePageCount = pageContainer.OrderedPagesIds.Count;
            if (request.PopCount > beforePageCount)
            { 
                throw new InvalidOperationException($"現在のページ数 {beforePageCount} より多い数 {request.PopCount} のページを閉じることはできません。");
            }

            await pageContainer.Pop(true, request.PopCount);
            if (beforePageCount > 0 && pageContainer.OrderedPagesIds.Count == 0)
            {
                lastPageClosed.OnNext(Unit.Default);
            }
        }
        
        void IDisposable.Dispose()
        {
            processCts.Cancel();
            processCts.Dispose();
        }

        internal sealed record PushRequest(ResourceKey ResourceKey, bool PlayAnimation, CancellationToken Ct)
        {
            public ResourceKey ResourceKey { get; } = ResourceKey;
            public bool PlayAnimation { get; } = PlayAnimation;
            public CancellationToken Ct { get; } = Ct;
        }

        internal sealed record PopRequest(int PopCount, CancellationToken Ct) 
        {
            public int PopCount { get; } = PopCount;
            public CancellationToken Ct { get; } = Ct;
        }
    }
}