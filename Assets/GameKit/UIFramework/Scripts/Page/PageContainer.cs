using System.Threading;
using Cysharp.Threading.Tasks;
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
        
        public async UniTask PushAsync<T>(bool playAnimation = true, CancellationToken ct = default) where T : BasePage 
        {
            var resourceKey = ResourceKey.FromGenerics<T>();
            var request = new PageContainerProcessor.PushRequest(resourceKey, playAnimation, ct);
            await pageContainerProcessor.PushAsync(request);
        }

        public async UniTask PopAsync(int popCount = 1, CancellationToken ct = default) 
        {
            var request = new PageContainerProcessor.PopRequest(popCount, ct);
            await pageContainerProcessor.PopAsync(request);
        }

        public async UniTask PopAllAsync(CancellationToken ct)
        {
            await PopAsync(popCount: pageContainer.OrderedPagesIds.Count, ct: ct);
        }

        public BasePage? GetActivePage()
        {
            if (pageContainer.OrderedPagesIds.Count == 0)
            {
                return null;
            }

            var id = pageContainer.OrderedPagesIds[^1];
            return pageContainer.Pages[id] as BasePage;
        }

        public bool IsEmpty()
        {
            return pageContainer.OrderedPagesIds.Count == 0;
        }
    }
}