namespace GameKit.Setting.SettingValue
{
    public sealed record IntSettingValue(int Value) : ISettingValue
    {
        public int Value { get; } = Value;
    }
}