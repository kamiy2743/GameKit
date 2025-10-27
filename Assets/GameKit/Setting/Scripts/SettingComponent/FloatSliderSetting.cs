using GameKit.Setting.SettingValue;
using GameKit.UIComponent.InputField;
using GameKit.UIComponent.Slider;
using R3;
using UnityEngine;

namespace GameKit.Setting.SettingComponent
{
    public sealed class FloatSliderSetting : MonoBehaviour, ISettingBindable<FloatSettingValue>
    {
        [SerializeField] FloatSlider slider;
        [SerializeField] FloatInputField inputField;

        void Start()
        {
            slider.OnValueChange()
                .Subscribe(x => inputField.SetValue(x))
                .AddTo(this);

            inputField.OnValueChange()
                .Subscribe(x => slider.SetValue(x))
                .AddTo(this);
        }
        
        void ISettingBindable<FloatSettingValue>.SetValue(FloatSettingValue value)
        {
            slider.SetValue(value.Value);
            inputField.SetValue(value.Value);
        }
        
        Observable<FloatSettingValue> ISettingBindable<FloatSettingValue>.OnValueChange()
        {
            return Observable.Merge(slider.OnValueChange(), inputField.OnValueChange())
                .Select(x => new FloatSettingValue(x));
        }
    }
}