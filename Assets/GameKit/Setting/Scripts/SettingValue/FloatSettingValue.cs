namespace GameKit.Setting.SettingValue
{
    public sealed record FloatSettingValue(float Value) : ISettingValue
    {
        public float Value { get; } = Value;
    }
}