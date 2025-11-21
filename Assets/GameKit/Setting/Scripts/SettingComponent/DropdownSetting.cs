using GameKit.Setting.SettingValue;
using GameKit.UIComponent.Dropdown;
using R3;
using UnityEngine;

namespace GameKit.Setting.SettingComponent
{
    public sealed class DropdownSetting : MonoBehaviour, ISettingBindable<IntSettingValue>
    {
        [SerializeField] Dropdown dropdown;
        
        public void SetUp(DropdownOptionList optionList)
        {
            dropdown.SetUp(optionList);
        }

        void ISettingBindable<IntSettingValue>.SetValue(IntSettingValue value)
        {
            dropdown.SetValue(value.Value);
        }

        Observable<IntSettingValue> ISettingBindable<IntSettingValue>.OnValueChange()
        {
            return dropdown.OnValueChange().Select(value => new IntSettingValue(value));
        }
    }
}