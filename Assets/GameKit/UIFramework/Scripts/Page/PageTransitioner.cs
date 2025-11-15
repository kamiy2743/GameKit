using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityScreenNavigator.Runtime.Core.Page;
using UnityScreenNavigator.Runtime.Core.Shared;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace GameKit.UIFramework.Page
{
    public sealed class PageTransitioner : IPostTickable, IDisposable
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
            
            if (
                request.AnimationMode == PageAnimationMode.Overlap &&
                pageContainer.OrderedPagesIds.Count > 0
            )
            {
                await ProcessPushOverlapAnimationAsync(request, ct);
            }
            else
            {
                await ProcessPushNormalAnimationAsync(request, ct);
            }
        }
        
        async UniTask ProcessPushNormalAnimationAsync(PushRequest request, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            if (request.AnimationMode == PageAnimationMode.Overlap)
            {
                throw new InvalidOperationException("通常アニメーションでPushする場合、Overlapアニメーションは指定できません。");
            }

            if (pageContainer.OrderedPagesIds.Count == 0)
            {
                willFirstPagePush.OnNext(Unit.Default);
            }

            var playAnimation = request.AnimationMode == PageAnimationMode.Play;
            await pageContainer.Push(request.PageName.ResourceKey, playAnimation);
        }
        
        async UniTask ProcessPushOverlapAnimationAsync(PushRequest request, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            if (request.AnimationMode != PageAnimationMode.Overlap)
            {
                throw new InvalidOperationException("OverlapアニメーションでPushする場合、通常アニメーションは指定できません。");
            }
            if (pageContainer.OrderedPagesIds.Count == 0)
            {
                throw new InvalidOperationException("最初のページをOverlapアニメーションで表示することはできません。");
            }
            
            var exitPage = pageContainer.Pages[pageContainer.OrderedPagesIds[^1]];
            var originalPushExitAnimations = new List<PageTransitionAnimationContainer.TransitionAnimation>(
                exitPage.AnimationContainer.PushExitAnimations
            );

            //TODO 開くページから取得
            var pushEnterAnimationDuration = 0.3f;
            var animation = exitPage.gameObject.AddComponent<PageOverlapAnimation>();
            animation.SetDuration(pushEnterAnimationDuration);
            exitPage.AnimationContainer.PushExitAnimations.Clear();
            exitPage.AnimationContainer.PushExitAnimations.Add(
                new PageTransitionAnimationContainer.TransitionAnimation
                {
                    AssetType = AnimationAssetType.MonoBehaviour,
                    AnimationBehaviour = animation,
                }
            );

            await pageContainer.Push(request.PageName.ResourceKey, true);

            Object.Destroy(animation);
            exitPage.AnimationContainer.PushExitAnimations.Clear();
            exitPage.AnimationContainer.PushExitAnimations.AddRange(originalPushExitAnimations);
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

        public sealed record PushRequest(PageName PageName, PageAnimationMode AnimationMode, CancellationToken Ct) {
            public PageName PageName { get; } = PageName;
            public PageAnimationMode AnimationMode { get; } = AnimationMode;
            public CancellationToken Ct { get; } = Ct;
        }

        public sealed record PopRequest(int PopCount, CancellationToken Ct) 
        {
            public int PopCount { get; } = PopCount;
            public CancellationToken Ct { get; } = Ct;
        }
    }
}