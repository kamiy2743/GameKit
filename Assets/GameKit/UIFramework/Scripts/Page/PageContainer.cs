using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace GameKit.UIFramework.Page
{
    public sealed class PageContainer
    {
        readonly UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer;

        readonly Subject<Unit> firstPageOpened = new();
        public Observable<Unit> FirstPageOpened => firstPageOpened;
        
        readonly Subject<Unit> lastPageClosed = new();
        public Observable<Unit> LastPageClosed => lastPageClosed;

        public PageContainer(UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer)
        {
            this.pageContainer = pageContainer;
        }

        public async UniTask PushAsync(PageName pageName, CancellationToken ct)
        {
            await PushAsync(pageName, true, ct);
        }
        
        public async UniTask PushWithoutAnimationAsync(PageName pageName, CancellationToken ct)
        {
            await PushAsync(pageName, false, ct);
        }
        
        async UniTask PushAsync(PageName pageName, bool playAnimation, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            await pageContainer.Push(pageName.ResourceKey, playAnimation);
            if (pageContainer.Pages.Count == 1)
            {
                firstPageOpened.OnNext(Unit.Default);
            }
        }
        
        public async UniTask PopAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var beforePageCount = pageContainer.Pages.Count;
            await pageContainer.Pop(true);
            if (beforePageCount == 1 && pageContainer.Pages.Count == 0)
            {
                lastPageClosed.OnNext(Unit.Default);
            }
        }
        
        public async UniTask PopAllAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var beforePageCount = pageContainer.Pages.Count;
            await pageContainer.Pop(true, pageContainer.Pages.Count);
            if (beforePageCount > 0)
            {
                lastPageClosed.OnNext(Unit.Default);
            }
        }
    }
}