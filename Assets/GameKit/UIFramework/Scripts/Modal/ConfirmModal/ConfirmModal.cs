using GameKit.Localization;
using GameKit.UIComponent.Button;
using GameKit.UIComponent.Text;
using R3;
using UnityEngine;

namespace GameKit.UIFramework.Modal.ConfirmModal
{
    public sealed class ConfirmModal : BaseModal
    {
        [SerializeField] Text messageText;
        [SerializeField] Button confirmButton;
        [SerializeField] Button cancelButton;
        
        public Observable<Unit> OnConfirmButtonClick() => confirmButton.OnClick();
        public Observable<Unit> OnCancelButtonClick() => cancelButton.OnClick();
        
        public void SetMessage(LocalizedString message)
        {
            messageText.SetText(message);
        }
    }
}