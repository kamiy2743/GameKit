namespace GameKit.Setting.SettingValue
{
    public sealed record DropdownSettingValue<T>(T Value) : ISettingValue
    {
        public T Value { get; } = Value;
    }
}