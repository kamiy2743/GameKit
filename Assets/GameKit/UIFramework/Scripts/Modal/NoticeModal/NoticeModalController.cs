using System.Threading;
using Cysharp.Threading.Tasks;
using GameKit.Localization;

namespace GameKit.UIFramework.Modal.NoticeModal
{
    public sealed class NoticeModalController
    {
        readonly ModalContainer modalContainer;
        readonly ModalStateHolder modalStateHolder;

        public NoticeModalController(
            ModalContainer modalContainer,
            ModalStateHolder modalStateHolder
        )
        {
            this.modalContainer = modalContainer;
            this.modalStateHolder = modalStateHolder;
        }
        
        public async UniTask PushAndWaitOkAsync(LocalizedString message, CancellationToken ct)
        {
            var modalId = await modalContainer.PushAsync(ModalName.Notice, ct: ct);
            await modalStateHolder.WaitForStateAsync(modalId, new NoticeModalState(true), ct);
        }
    }
}
