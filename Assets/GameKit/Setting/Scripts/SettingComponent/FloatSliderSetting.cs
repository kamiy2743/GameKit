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

            inputField.OnEndEdit()
                .Subscribe(x =>
                {
                    slider.SetValue(x);
                    inputField.SetValue(slider.Value);
                })
                .AddTo(this);
        }

        public void SetUp(float min, float max)
        {
            slider.SetRange(min, max);
        }
        
        void ISettingBindable<FloatSettingValue>.SetValue(FloatSettingValue value)
        {
            slider.SetValue(value.Value);
            inputField.SetValue(slider.Value);
        }
        
        Observable<FloatSettingValue> ISettingBindable<FloatSettingValue>.OnValueChange()
        {
            return slider.OnValueChange().Select(x => new FloatSettingValue(x));
        }
    }
}