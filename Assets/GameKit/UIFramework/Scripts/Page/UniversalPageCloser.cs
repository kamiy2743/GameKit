using System;
using R3;
using VContainer.Unity;

namespace GameKit.UIFramework.Page
{
    public sealed class UniversalPageCloser : IInitializable, IDisposable
    {
        readonly IUniversalClosePageObservable universalClosePageObservable;
        readonly PageContainer pageContainer;

        readonly CompositeDisposable disposable = new();

        public UniversalPageCloser(
            IUniversalClosePageObservable universalClosePageObservable,
            PageContainer pageContainer
        )
        {
            this.universalClosePageObservable = universalClosePageObservable;
            this.pageContainer = pageContainer;
        }
        
        void IInitializable.Initialize()
        {
            universalClosePageObservable.OnCloseRequest()
                .Where(_ => pageContainer.GetActivePage()?.AllowUniversalClose() ?? false)
                .SubscribeAwait(async (_, c) =>
                {
                    await pageContainer.PopAsync(ct: c);
                })
                .AddTo(disposable);
        }

        void IDisposable.Dispose()
        {
            disposable.Dispose();
        }
    }
}