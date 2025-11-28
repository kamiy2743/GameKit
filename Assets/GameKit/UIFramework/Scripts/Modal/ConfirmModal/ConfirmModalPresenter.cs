using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace GameKit.UIFramework.Modal.ConfirmModal
{
    public sealed class ConfirmModalPresenter : BaseModalPresenter
    {
        readonly ConfirmModal modal;
        readonly ModalContainer modalContainer;
        readonly ModalStateHolder modalStateHolder;

        public ConfirmModalPresenter(
            ConfirmModal modal,
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
            modalStateHolder.Update(modal.GetId(), ConfirmModalState.Pending);
            
            modal.OnConfirmButtonClick()
                .SubscribeAwait(async (_, c) =>
                {
                    modalStateHolder.Update(modal.GetId(), ConfirmModalState.Ok);
                    await modalContainer.PopAsync(ct: c);
                })
                .AddTo(ct);
            
            modal.OnCancelButtonClick()
                .SubscribeAwait(async (_, c) =>
                {
                    await modalContainer.PopAsync(ct: c);
                })
                .AddTo(ct);
        }

        protected override async UniTask WillPopExitAsync(CancellationToken ct)
        {
            var state = modalStateHolder.Get<ConfirmModalState>(modal.GetId());
            if (state.Equals(ConfirmModalState.Pending))
            {
                modalStateHolder.Update(modal.GetId(), ConfirmModalState.Cancel);
            }
        }
    }
}