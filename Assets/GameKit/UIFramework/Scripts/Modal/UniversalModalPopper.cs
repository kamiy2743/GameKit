using System;
using R3;
using VContainer.Unity;

namespace GameKit.UIFramework.Modal
{
    public sealed class UniversalModalPopper : IInitializable, IDisposable
    {
        readonly IUniversalPopModalObservable universalPopModalObservable;
        readonly ModalContainer modalContainer;

        readonly CompositeDisposable disposable = new();

        public UniversalModalPopper(
            IUniversalPopModalObservable universalPopModalObservable,
            ModalContainer modalContainer
        )
        {
            this.universalPopModalObservable = universalPopModalObservable;
            this.modalContainer = modalContainer;
        }
        
        void IInitializable.Initialize()
        {
            universalPopModalObservable.OnPopRequest()
                .Where(_ => CanPopModal())
                .SubscribeAwait(async (_, c) =>
                {
                    await modalContainer.PopAsync(ct: c);
                })
                .AddTo(disposable);
        }
        
        bool CanPopModal()
        {
            if (modalContainer.IsTransitioning())
            {
                return false;
            }
            
            if (!(modalContainer.GetActiveModal()?.AllowUniversalPop() ?? false))
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