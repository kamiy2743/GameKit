using GameKit.Setting;

namespace GameKit.ScreenResolution.ScreenResolutionSetting
{
    public sealed class ScreenResolutionSettingProperty : ISettingProperty<ScreenResolutionSettingValue>
    {
        public ScreenResolutionSettingValue Default => new(ScreenResolution.SR_1920x1080);
    }
}