using GameKit.Setting.SettingValue;

namespace GameKit.ScreenResolution.ScreenResolutionSetting
{
    public sealed record ScreenResolutionSettingValue(ScreenResolution Value) : IDropdownSettingValue<ScreenResolution>
    {
        public ScreenResolution Value { get; } = Value;
    }
}