using System;
using R3;
using VContainer.Unity;

namespace GameKit.UIFramework.Page
{
    public sealed class UniversalPageCloser : IInitializable, IDisposable
    {
        readonly IUniversalClosePageObservable universalClosePageObservable;
        readonly PageContainer pageContainer;
        readonly UniversalClosePageSpecification universalClosePageSpecification;

        readonly CompositeDisposable disposable = new();

        public UniversalPageCloser(
            IUniversalClosePageObservable universalClosePageObservable,
            PageContainer pageContainer,
            UniversalClosePageSpecification universalClosePageSpecification
        )
        {
            this.universalClosePageObservable = universalClosePageObservable;
            this.pageContainer = pageContainer;
            this.universalClosePageSpecification = universalClosePageSpecification;
        }
        
        void IInitializable.Initialize()
        {
            universalClosePageObservable.OnCloseRequest()
                .Where(_ => universalClosePageSpecification.Check())
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