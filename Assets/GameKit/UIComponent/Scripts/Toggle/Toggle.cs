using R3;
using UnityEngine;

namespace GameKit.UIComponent.Toggle
{
    public sealed class Toggle : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Slider slider;
        
        public void SetValue(bool value)
        {
            slider.value = value ? 1 : 0;
        }
        
        public Observable<bool> OnValueChange()
        {
            return slider.OnValueChangedAsObservable().Select(x => x > 0.5f);
        }
    }
}