using GameKit.LocalStorage;

namespace GameKit.Setting.SettingValue
{
    public sealed record BoolSettingValue(bool Value) : ISettingValue
    {
        public bool Value { get; } = Value;

        string ILocalStorageValue.ToStringValue()
        {
            return Value.ToString();
        }

        T ILocalStorageValue.FromStringValue<T>(string value)
        {
            bool parsedValue = bool.Parse(value);
            return (T)(ILocalStorageValue)new BoolSettingValue(parsedValue);
        }
    }
}