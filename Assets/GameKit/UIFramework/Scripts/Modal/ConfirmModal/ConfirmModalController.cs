using System.Threading;
using Cysharp.Threading.Tasks;
using GameKit.Localization;

namespace GameKit.UIFramework.Modal.ConfirmModal
{
    public sealed class ConfirmModalController
    {
        readonly ModalContainer modalContainer;
        readonly ModalStateHolder modalStateHolder;

        public ConfirmModalController(
            ModalContainer modalContainer,
            ModalStateHolder modalStateHolder
        )
        {
            this.modalContainer = modalContainer;
            this.modalStateHolder = modalStateHolder;
        }
        
        public async UniTask<bool> PushAndWaitResultAsync(LocalizedString message, CancellationToken ct)
        {
            var pushModalParams = new PushConfirmModalParams(message);
            var modalId = await modalContainer.PushAsync(ModalName.Confirm, pushModalParams, ct: ct);
            var result = await UniTask.WhenAny(
                modalStateHolder.WaitForStateAsync(modalId, ConfirmModalState.Ok, ct),
                modalStateHolder.WaitForStateAsync(modalId, ConfirmModalState.Cancel, ct)
            );
            return result == 0;
        }
    }
}