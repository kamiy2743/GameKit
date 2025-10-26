namespace GameKit.Setting.SettingValue
{
    public sealed record BoolSettingValue(bool Value) : ISettingValue
    {
        public bool Value { get; } = Value;
    }
}