using GameKit.Localization;
using GameKit.UIComponent.Button;
using GameKit.UIComponent.Text;
using R3;
using UnityEngine;

namespace GameKit.UIFramework.Modal.NoticeModal
{
    public sealed class NoticeModal : BaseModal
    {
        [SerializeField] Text messageText;
        [SerializeField] Button okButton;

        public Observable<Unit> OnOkButtonClick() => okButton.OnClick();
        
        public void SetMessage(LocalizedString message)
        {
            messageText.SetText(message);
        }
    }
}