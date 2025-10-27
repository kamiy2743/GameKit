using R3;
using UnityEngine;

namespace GameKit.UIComponent.Slider
{
    public sealed class FloatSlider : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Slider slider;
        
        public void SetValue(float value)
        {
            slider.value = value;
        }
        
        public Observable<float> OnValueChange()
        {
            return Observable.EveryUpdate()
                .Select(_ => slider.value)
                .DistinctUntilChanged();
        }
    }
}