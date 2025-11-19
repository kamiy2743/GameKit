using GameKit.UIComponent.Button;
using GameKit.UIFramework.Modal;
using R3;
using UnityEngine;

namespace GameKit.License.LicenseModal
{
    public sealed class LicenseModal : BaseModal
    {
        [SerializeField] Button closeButton;
        
        public Observable<Unit> OnCloseButtonClick() => closeButton.OnClick();
    }
}