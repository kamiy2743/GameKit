using GameKit.LocalStorage;

namespace GameKit.Setting.SettingValue
{
    public sealed record StringSettingValue(string Value) : ISettingValue
    {
        public string Value { get; } = Value;
        
        string ILocalStorageValue.Serialize()
        {
            return Value;
        }
        
        T ILocalStorageValue.Deserialize<T>(string value)
        {
            return (T)(ILocalStorageValue)new StringSettingValue(value);
        }
    }
}