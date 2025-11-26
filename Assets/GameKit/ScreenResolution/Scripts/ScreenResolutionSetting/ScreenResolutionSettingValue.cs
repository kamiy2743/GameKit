using GameKit.LocalStorage;
using GameKit.Setting.SettingValue;

namespace GameKit.ScreenResolution.ScreenResolutionSetting
{
    public sealed record ScreenResolutionSettingValue(ScreenResolution Value) : IDropdownSettingValue<ScreenResolution>
    {
        public ScreenResolution Value { get; } = Value;
        
        string ILocalStorageValue.Serialize()
        {
            return Value.Identifier;
        }

        T ILocalStorageValue.Deserialize<T>(string value)
        {
            var screenResolution = ScreenResolution.FromIdentifier(value);
            return (T)(ILocalStorageValue)new ScreenResolutionSettingValue(screenResolution);
        }
    }
}