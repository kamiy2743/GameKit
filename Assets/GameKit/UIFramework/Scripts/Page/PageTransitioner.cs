using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameKit.UIFramework.UnityScreenNavigatorResource;
using R3;
using VContainer.Unity;

namespace GameKit.UIFramework.Page
{
    sealed class PageTransitioner : IPostTickable, IDisposable
    {
        readonly UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer;
        
        readonly Subject<Unit> willFirstPageOpen = new();
        public Observable<Unit> WillFirstPageOpen => willFirstPageOpen;
        
        readonly Subject<Unit> willLastPageClose = new();
        public Observable<Unit> WillLastPageClose => willLastPageClose;
        
        readonly CancellationTokenSource processCts = new();
        
        PushRequest? currentPushRequest;
        PopRequest? currentPopRequest;
        bool isProcessing;

        public PageTransitioner(UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer)
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
        
        public bool IsTransitioning()
        {
            return isProcessing || currentPushRequest != null || currentPopRequest != null;
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

            if (pageContainer.OrderedPagesIds.Count == 0)
            {
                willFirstPageOpen.OnNext(Unit.Default);
            }
            await pageContainer.Push(request.ResourceKey.Value, request.PlayAnimation);
        }
        
        async UniTask ProcessPopAsync(PopRequest request, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var pageCount = pageContainer.OrderedPagesIds.Count;
            if (request.PopCount > pageCount)
            { 
                throw new InvalidOperationException($"現在のページ数 {pageCount} より多い数 {request.PopCount} のページを閉じることはできません。");
            }

            if (request.PopCount == pageCount)
            {
                willLastPageClose.OnNext(Unit.Default);
            }
            await pageContainer.Pop(true, request.PopCount);
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