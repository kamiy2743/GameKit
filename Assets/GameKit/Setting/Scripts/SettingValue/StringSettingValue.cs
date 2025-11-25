using GameKit.LocalStorage;

namespace GameKit.Setting.SettingValue
{
    public sealed record StringSettingValue(string Value) : ISettingValue
    {
        public string Value { get; } = Value;
        
        string ILocalStorageValue.ToStringValue()
        {
            return Value;
        }
        
        T ILocalStorageValue.FromStringValue<T>(string value)
        {
            return (T)(ILocalStorageValue)new StringSettingValue(value);
        }
    }
}