using R3;
using UnityEngine;

namespace GameKit.UIComponent.Slider
{
    public sealed class FloatSlider : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Slider slider;
        
        public float Value => slider.value;
        public float Min => slider.minValue;
        public float Max => slider.maxValue;
        
        public void SetRange(float min, float max)
        {
            if (min >= max)
            {
                throw new System.ArgumentException("最小値は最大値未満である必要があります。");
            }
            
            slider.minValue = min;
            slider.maxValue = max;
        }
        
        public void SetValue(float value)
        {
            slider.value = Mathf.Clamp(value, Min, Max);
        }
        
        public Observable<float> OnValueChange()
        {
            return slider.OnValueChangedAsObservable();
        }
    }
}