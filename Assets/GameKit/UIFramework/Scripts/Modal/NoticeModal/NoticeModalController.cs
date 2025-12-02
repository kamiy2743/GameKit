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
            var pushModalParams = new PushNoticeModalParams(message);
            var modalId = await modalContainer.PushAsync(ModalName.Notice, pushModalParams, ct: ct);
            await modalStateHolder.WaitForStateAsync(modalId, new NoticeModalState(true), ct);
        }
    }
}
