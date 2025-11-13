using System;
using R3;
using VContainer.Unity;

namespace GameKit.UIFramework.Page
{
    public sealed class UniversalPagePopper : IInitializable, IDisposable
    {
        readonly IUniversalPopPageObservable universalPopPageObservable;
        readonly PageContainer pageContainer;

        readonly CompositeDisposable disposable = new();

        public UniversalPagePopper(
            IUniversalPopPageObservable universalPopPageObservable,
            PageContainer pageContainer
        )
        {
            this.universalPopPageObservable = universalPopPageObservable;
            this.pageContainer = pageContainer;
        }
        
        void IInitializable.Initialize()
        {
            universalPopPageObservable.OnPopRequest()
                .Where(_ => CanPopPage())
                .SubscribeAwait(async (_, c) =>
                {
                    await pageContainer.PopAsync(ct: c);
                })
                .AddTo(disposable);
        }
        
        bool CanPopPage()
        {
            if (pageContainer.IsTransitioning())
            {
                return false;
            }
            
            if (!pageContainer.GetActivePage()?.AllowUniversalPop() ?? false)
            {
                return false;
            }
            
            return true;
        }

        void IDisposable.Dispose()
        {
            disposable.Dispose();
        }
    }
}