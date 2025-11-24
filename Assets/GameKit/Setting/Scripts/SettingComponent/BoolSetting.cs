using GameKit.Setting.SettingValue;
using GameKit.UIComponent.Toggle;
using R3;
using UnityEngine;

namespace GameKit.Setting.SettingComponent
{
    public sealed class BoolSetting : MonoBehaviour, ISettingBindable<BoolSettingValue>
    {
        [SerializeField] Toggle toggle;
        
        void ISettingBindable<BoolSettingValue>.SetValue(BoolSettingValue value)
        {
            toggle.SetValue(value.Value);
        }

        Observable<BoolSettingValue> ISettingBindable<BoolSettingValue>.OnValueChange()
        {
            return toggle.OnValueChange().Select(x => new BoolSettingValue(x));
        }
    }
}