using GameKit.Setting.SettingValue;
using GameKit.UIComponent.InputField;
using GameKit.UIComponent.Slider;
using R3;
using UnityEngine;

namespace GameKit.Setting.SettingComponent
{
    public sealed class IntSliderSetting : MonoBehaviour, ISettingBindable<IntSettingValue>
    {
        [SerializeField] IntSlider slider;
        [SerializeField] IntInputField inputField;
        
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
        
        public void SetUp(int min, int max, int step = 1)
        {
            slider.SetRange(min, max, step);
        }
        
        void ISettingBindable<IntSettingValue>.SetValue(IntSettingValue value)
        {
            slider.SetValue(value.Value);
            inputField.SetValue(slider.Value);
        }
        
        Observable<IntSettingValue> ISettingBindable<IntSettingValue>.OnValueChange()
        {
            return slider.OnValueChange().Select(x => new IntSettingValue(x));
        }
    }
}