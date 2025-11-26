using GameKit.LocalStorage;

namespace GameKit.Setting.SettingValue
{
    public sealed record BoolSettingValue(bool Value) : ISettingValue
    {
        public bool Value { get; } = Value;

        string ILocalStorageValue.Serialize()
        {
            return Value.ToString();
        }

        T ILocalStorageValue.Deserialize<T>(string value)
        {
            bool parsedValue = bool.Parse(value);
            return (T)(ILocalStorageValue)new BoolSettingValue(parsedValue);
        }
    }
}