using GameKit.UIComponent.Text;
using UnityEngine;

namespace GameKit.License
{
    public sealed class LicenseContent : MonoBehaviour
    {
        [SerializeField] Text nameText;
        [SerializeField] Text bodyText;
        
        public void SetValue(LicenseContentValue value)
        {
            nameText.SetPlainText(value.Name);
            bodyText.SetPlainText(value.Body);
        }
    }
}