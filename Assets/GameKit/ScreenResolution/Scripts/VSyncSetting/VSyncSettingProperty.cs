using GameKit.Setting;
using GameKit.Setting.SettingValue;

namespace GameKit.ScreenResolution.VSyncSetting
{
    public sealed record VSyncSettingProperty : ISettingProperty<BoolSettingValue>
    {
        public BoolSettingValue Default => new(true);
    }
}