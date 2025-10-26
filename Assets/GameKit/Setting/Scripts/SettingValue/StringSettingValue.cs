namespace GameKit.Setting.SettingValue
{
    public sealed record StringSettingValue(string Value) : ISettingValue
    {
        public string Value { get; } = Value;
    }
}