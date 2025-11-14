using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace GameKit.UIFramework.Page
{
    sealed class PageTransitioner : IPostTickable, IDisposable
    {
        readonly UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer;
        
        readonly Subject<Unit> willFirstPagePush = new();
        public Observable<Unit> WillFirstPagePush => willFirstPagePush;
        
        readonly Subject<Unit> willLastPagePop = new();
        public Observable<Unit> WillLastPagePop => willLastPagePop;
        
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
            if (isProcessing)
            {
                throw new InvalidOperationException($"画面遷移中のため、Push要求を処理できません。 {request}, {currentPushRequest}, {currentPopRequest}");
            }
            if (currentPushRequest != null)
            {
                throw new InvalidOperationException($"他のPush処理が待機しているため、Push要求を処理できません。 {request}, {currentPushRequest}");
            }

            currentPushRequest = request;
            currentPopRequest = null;
            await UniTask.WaitUntil(() => currentPushRequest == null, cancellationToken: request.Ct);
        }
        
        public async UniTask PopAsync(PopRequest request)
        {
            if (isProcessing)
            {
                throw new InvalidOperationException($"画面遷移中のため、Push要求を処理できません。 {request}, {currentPushRequest}, {currentPopRequest}");
            }
            if (currentPushRequest != null || currentPopRequest != null)
            {
                throw new InvalidOperationException($"他のPushまたはPop処理が待機しているため、Pop要求を処理できません。 {request}, {currentPushRequest}, {currentPopRequest}");
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
                willFirstPagePush.OnNext(Unit.Default);
            }
            var playAnimation =
                request.AnimationMode == PageAnimationMode.Play ||
                request.AnimationMode == PageAnimationMode.Overlap;
            await pageContainer.Push(request.PageName.ResourceKey, playAnimation);
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
                willLastPagePop.OnNext(Unit.Default);
            }

            var popTargetPages = pageContainer.OrderedPagesIds
                .TakeLast(request.PopCount)
                .Select(id => pageContainer.Pages[id])
                // 一番手前以外を取得
                .Take(request.PopCount - 1);
            foreach (var page in popTargetPages)
            {
                page.GetComponent<Canvas>().enabled = false;
            }
            
            await pageContainer.Pop(true, request.PopCount);
        }
        
        void IDisposable.Dispose()
        {
            processCts.Cancel();
            processCts.Dispose();
        }

        internal sealed record PushRequest(PageName PageName, PageAnimationMode AnimationMode, CancellationToken Ct) {
            public PageName PageName { get; } = PageName;
            public PageAnimationMode AnimationMode { get; } = AnimationMode;
            public CancellationToken Ct { get; } = Ct;
        }

        internal sealed record PopRequest(int PopCount, CancellationToken Ct) 
        {
            public int PopCount { get; } = PopCount;
            public CancellationToken Ct { get; } = Ct;
        }
    }
}