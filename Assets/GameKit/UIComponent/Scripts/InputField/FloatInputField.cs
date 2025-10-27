using System.Globalization;
using R3;
using TMPro;
using UnityEngine;

namespace GameKit.UIComponent.InputField
{
    public sealed class FloatInputField : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputField;
        
        public void SetValue(float value)
        {
            inputField.text = value.ToString(CultureInfo.CurrentCulture);
        }
        
        public Observable<float> OnValueChange()
        {
            return Observable.EveryUpdate()
                .Select(_ => float.TryParse(inputField.text, out var result) ? result : 0f)
                .DistinctUntilChanged();
        }
    }
}