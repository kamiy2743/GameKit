using TMPro;
using UnityEngine;

namespace GameKit.UIComponent.Text
{
    public sealed class Text : MonoBehaviour
    {
        [SerializeField] TMP_Text text;
        
        public void SetText(string value)
        {
            text.text = value;
        }
    }
}