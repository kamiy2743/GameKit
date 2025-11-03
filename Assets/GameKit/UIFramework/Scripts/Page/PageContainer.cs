using System.Threading;
using Cysharp.Threading.Tasks;
using GameKit.Exception;
using GameKit.UIFramework.UnityScreenNavigatorResource;
using R3;

namespace GameKit.UIFramework.Page
{
    public sealed class PageContainer
    {
        readonly UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer;
        readonly PageContainerProcessor pageContainerProcessor;

        public Observable<Unit> FirstPageOpened => pageContainerProcessor.FirstPageOpened;
        public Observable<Unit> LastPageClosed => pageContainerProcessor.LastPageClosed;

        internal PageContainer(
            UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer,
            PageContainerProcessor pageContainerProcessor
        )
        {
            this.pageContainer = pageContainer;
            this.pageContainerProcessor = pageContainerProcessor;
        }
        
        public async UniTask PushAsync<T>(
            bool playAnimation = true,
            ExceptionHandleMode modeWhenConcurrentRequested = ExceptionHandleMode.Throw,
            CancellationToken ct = default
        ) where T : BasePage 
        {
            var resourceKey = ResourceKey.FromGenerics<T>();
            var request = new PageContainerProcessor.PushRequest(
                resourceKey,
                playAnimation,
                modeWhenConcurrentRequested,
                ct
            );
            await pageContainerProcessor.PushAsync(request);
        }

        public async UniTask PopAsync(
            int popCount = 1,
            ExceptionHandleMode modeWhenConcurrentRequested = ExceptionHandleMode.Throw,
            ExceptionHandleMode modeWhenPopCountExceeded = ExceptionHandleMode.Throw,
            CancellationToken ct = default
        ) 
        {
            var request = new PageContainerProcessor.PopRequest(
                popCount, 
                modeWhenConcurrentRequested, 
                modeWhenPopCountExceeded, 
                ct
            );
            await pageContainerProcessor.PopAsync(request);
        }

        public async UniTask PopAllAsync(CancellationToken ct)
        {
            await PopAsync(popCount: pageContainer.Pages.Count, ct: ct);
        }

        public bool IsEmpty()
        {
            return pageContainer.Pages.Count == 0;
        }
    }
}