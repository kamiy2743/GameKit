using System.Threading;
using Cysharp.Threading.Tasks;
using GameKit.Lock;
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
        
        readonly AsyncLocker locker = new();

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
            using (await locker.LockAsync(ct))
            {
                await pageContainer.Push(pageName.ResourceKey, playAnimation);
                if (pageContainer.Pages.Count == 1)
                {
                    firstPageOpened.OnNext(Unit.Default);
                }
            }
        }
        
        public async UniTask PopAsync(CancellationToken ct)
        {
            await PopAsync(1, ct);
        }
        
        public async UniTask PopAllAsync(CancellationToken ct)
        {
            await PopAsync(pageContainer.Pages.Count, ct);
        }

        async UniTask PopAsync(int popCount, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            using (await locker.LockAsync(ct))
            {
                var beforePageCount = pageContainer.Pages.Count;
                await pageContainer.Pop(true, popCount);
                if (beforePageCount > 0 && pageContainer.Pages.Count == 0)
                {
                    lastPageClosed.OnNext(Unit.Default);
                }
            }
        }

        public async UniTask<bool> IsEmptyAsync(CancellationToken ct)
        {
            using (await locker.LockAsync(ct))
            {
                return pageContainer.Pages.Count == 0;
            }
        }
    }
}