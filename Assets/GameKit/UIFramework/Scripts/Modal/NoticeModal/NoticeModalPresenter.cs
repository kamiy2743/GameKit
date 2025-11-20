using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace GameKit.UIFramework.Modal.NoticeModal
{
    public sealed class NoticeModalPresenter : BaseModalPresenter
    {
        readonly NoticeModal modal;
        readonly ModalContainer modalContainer;
        readonly ModalStateHolder modalStateHolder;

        public NoticeModalPresenter(
            NoticeModal modal,
            ModalContainer modalContainer,
            ModalStateHolder modalStateHolder
        )
        {
            this.modal = modal;
            this.modalContainer = modalContainer;
            this.modalStateHolder = modalStateHolder;
        }
        
        protected override async UniTask InitializeAsync(CancellationToken ct)
        {
            modalStateHolder.Update(modal.GetId(), new NoticeModalState(false));
            
            modal.OnOkButtonClick()
                .SubscribeAwait(async (_, c) =>
                {
                    await modalContainer.PopAsync(ct: c);
                })
                .AddTo(ct);
        }

        protected override async UniTask WillPopExitAsync(CancellationToken ct)
        {
            modalStateHolder.Update(modal.GetId(), new NoticeModalState(true));
        }
    }
}