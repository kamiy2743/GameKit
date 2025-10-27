using GameKit.UIComponent.InputField;
using GameKit.UIComponent.Slider;
using UnityEngine;

namespace GameKit.Setting.SettingComponent
{
    public sealed class IntSliderSetting : MonoBehaviour
    {
        [SerializeField] IntSlider slider;
        [SerializeField] IntInputField inputField;
    }
}