using GameKit.UIComponent.Button;
using R3;
using UnityEngine;

namespace GameKit.UIFramework.Modal.ConfirmModal
{
    public sealed class ConfirmModal : BaseModal
    {
        [SerializeField] Button confirmButton;
        [SerializeField] Button cancelButton;
        
        public Observable<Unit> OnConfirmButtonClick() => confirmButton.OnClick();
        public Observable<Unit> OnCancelButtonClick() => cancelButton.OnClick();
    }
}