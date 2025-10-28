using GameKit.UIComponent.Text;
using UnityEngine;

namespace GameKit.License
{
    public sealed class LicenseContent : MonoBehaviour
    {
        [SerializeField] Text nameText;
        [SerializeField] Text bodyText;
        
        public void SetContent(string name, string body)
        {
            nameText.SetText(name);
            bodyText.SetText(body);
        }
    }
}