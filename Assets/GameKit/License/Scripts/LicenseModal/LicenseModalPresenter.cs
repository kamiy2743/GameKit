using System.Threading;
using Cysharp.Threading.Tasks;
using GameKit.UIFramework.Modal;
using R3;

namespace GameKit.License.LicenseModal
{
    public sealed class LicenseModalPresenter : BaseModalPresenter
    {
        readonly LicenseModal modal;
        readonly ModalContainer modalContainer;

        public LicenseModalPresenter(
            LicenseModal modal,
            ModalContainer modalContainer
        )
        {
            this.modal = modal;
            this.modalContainer = modalContainer;
        }

        protected override async UniTask InitializeAsync(CancellationToken ct)
        {
            modal.OnCloseButtonClick()
                .SubscribeAwait(async (_, c) =>
                {
                    await modalContainer.PopAsync(ct: c);
                })
                .AddTo(ct);
        }
    }
}