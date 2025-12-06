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
                    inputField.SetValue(Mathf.Clamp(x, slider.Min, slider.Max));
                })
                .AddTo(this);
        }
        
        public void SetUp(int min, int max)
        {
            slider.SetRange(min, max);
        }
        
        void ISettingBindable<IntSettingValue>.SetValue(IntSettingValue value)
        {
            slider.SetValue(value.Value);
            inputField.SetValue(value.Value);
        }
        
        Observable<IntSettingValue> ISettingBindable<IntSettingValue>.OnValueChange()
        {
            return Observable.Merge(slider.OnValueChange(), inputField.OnEndEdit())
                .Select(x => new IntSettingValue(x));
        }
    }
}