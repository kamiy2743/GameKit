namespace GameKit.Setting.SettingValue
{
    public interface IDropdownSettingValue<T> : ISettingValue
    {
        T Value { get; }
    }
}