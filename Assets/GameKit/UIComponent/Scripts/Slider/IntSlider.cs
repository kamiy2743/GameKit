using R3;
using UnityEngine;

namespace GameKit.UIComponent.Slider
{
    public sealed class IntSlider : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Slider slider;
        
        public int Min => (int)slider.minValue;
        public int Max => (int)slider.maxValue;
        
        public void SetRange(int min, int max)
        {
            slider.minValue = min;
            slider.maxValue = max;
        }
        
        public void SetValue(int value)
        {
            slider.value = value;
        }
        
        public Observable<int> OnValueChange()
        {
            return slider.OnValueChangedAsObservable().Select(x => (int)x);
        }
    }
}