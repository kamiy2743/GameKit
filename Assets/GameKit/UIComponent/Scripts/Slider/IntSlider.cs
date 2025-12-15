using System;
using R3;
using UnityEngine;

namespace GameKit.UIComponent.Slider
{
    public sealed class IntSlider : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Slider slider;
        
        int step = 1;

        public int Value => (int)(slider.value * step);
        public int Min => (int)slider.minValue * step;
        public int Max => (int)slider.maxValue * step;
        
        public void SetRange(int min, int max, int step = 1)
        {
            if (min >= max)
            {
                throw new ArgumentException("最小値は最大値未満である必要があります。");
            }
            if (min % step != 0 || max % step != 0)
            {
                throw new ArgumentException("最小値と最大値はステップの倍数である必要があります。");
            }
            
            this.step = step;
            slider.minValue = min / step;
            slider.maxValue = max / step;
        }
        
        public void SetValue(int value)
        {
            var clampedValue = Mathf.Clamp(value, Min, Max);
            slider.value = Mathf.RoundToInt((float)clampedValue / step);
        }
        
        public Observable<int> OnValueChange()
        {
            return slider.OnValueChangedAsObservable().Select(x => (int)(x * step));
        }
    }
}