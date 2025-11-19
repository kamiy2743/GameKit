using GameKit.UIComponent.Button;
using GameKit.UIFramework.Modal;
using R3;
using UnityEngine;

namespace GameKit.License.LicenseModal
{
    public sealed class LicenseModal : BaseModal
    {
        //TODO closebutton
        [SerializeField] Button backButton;
        
        public Observable<Unit> OnBackButtonClick() => backButton.OnClick();
    }
}