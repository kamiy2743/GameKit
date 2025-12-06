using R3;
using TMPro;
using UnityEngine;

namespace GameKit.UIComponent.InputField
{
    public sealed class IntInputField : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputField;
        
        public void SetValue(int value)
        {
            inputField.text = value.ToString();
        }
        
        public Observable<int> OnEndEdit()
        {
            return inputField.OnEndEditAsObservable()
                .Select(Parse);
        }
        
        int Parse(string text)
        {
            var floatValue = float.TryParse(text, out var floatResult) ? floatResult : 0f;
            return Mathf.CeilToInt(floatValue);
        }
    }
}