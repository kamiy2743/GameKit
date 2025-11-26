using GameKit.LocalStorage.LocalStorageValue;
using GameKit.Setting.SettingValue;

namespace GameKit.ScreenResolution.ScreenResolutionSetting
{
    public sealed record ScreenResolutionSettingValue(ScreenResolution Value)
        : IDropdownSettingValue<ScreenResolution>, IStringLocalStorageValue<ScreenResolutionSettingValue>
    {
        public ScreenResolution Value { get; } = Value;

        string IStringLocalStorageValue<ScreenResolutionSettingValue>.Serialize()
        {
            return Value.Identifier;
        }

        ScreenResolutionSettingValue IStringLocalStorageValue<ScreenResolutionSettingValue>.Deserialize(string value)
        {
            var screenResolution = ScreenResolution.FromIdentifier(value);
            return new ScreenResolutionSettingValue(screenResolution);
        }
    }
}