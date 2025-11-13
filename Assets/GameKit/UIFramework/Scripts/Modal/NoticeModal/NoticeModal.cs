using GameKit.UIComponent.Button;
using R3;
using UnityEngine;

namespace GameKit.UIFramework.Modal.NoticeModal
{
    public sealed class NoticeModal : BaseModal
    {
        [SerializeField] Button okButton;

        public Observable<Unit> OnOkButtonClick() => okButton.OnClick();
    }
}