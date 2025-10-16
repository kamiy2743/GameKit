using System.Threading;
using Cysharp.Threading.Tasks;

namespace GameKit.UIFramework.Page
{
    public sealed class PageContainer
    {
        readonly UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer;

        public PageContainer(UnityScreenNavigator.Runtime.Core.Page.PageContainer pageContainer)
        {
            this.pageContainer = pageContainer;
        }

        public async UniTask PushAsync(PageName pageName, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            await pageContainer.Push(pageName.ResourceKey, true);
        }
        
        public async UniTask PushWithoutAnimationAsync(PageName pageName, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            await pageContainer.Push(pageName.ResourceKey, false);
        }
        
        public async UniTask PopAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            await pageContainer.Pop(true);
        }
    }
}