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
            nameText.SetText(value.Name);
            bodyText.SetText(value.Body);
        }
    }
}