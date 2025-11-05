using System.Threading;
using Cysharp.Threading.Tasks;
using GameKit.UIFramework.UnityScreenNavigatorResource;
using R3;

namespace GameKit.UIFramework.Page
{
    public sealed class PageContainer
    {
        readonly UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer;
        readonly PageTransitioner pageTransitioner;

        public Observable<Unit> FirstPageOpened => pageTransitioner.FirstPageOpened;
        public Observable<Unit> LastPageClosed => pageTransitioner.LastPageClosed;

        internal PageContainer(
            UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer,
            PageTransitioner pageTransitioner
        )
        {
            this.pageContainer = pageContainer;
            this.pageTransitioner = pageTransitioner;
        }
        
        public async UniTask PushAsync<T>(bool playAnimation = true, CancellationToken ct = default) where T : BasePage 
        {
            var resourceKey = ResourceKey.FromGenerics<T>();
            var request = new PageTransitioner.PushRequest(resourceKey, playAnimation, ct);
            await pageTransitioner.PushAsync(request);
        }

        public async UniTask PopAsync(int popCount = 1, CancellationToken ct = default) 
        {
            var request = new PageTransitioner.PopRequest(popCount, ct);
            await pageTransitioner.PopAsync(request);
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
        
        public bool IsTransitioning()
        {
            return pageTransitioner.IsTransitioning();
        }
    }
}