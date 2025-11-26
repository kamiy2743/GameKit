using GameKit.LocalStorage.LocalStorageValue;

namespace GameKit.Setting.SettingValue
{
    public sealed record StringSettingValue(string Value) : ISettingValue, IStringLocalStorageValue<StringSettingValue>
    {
        public string Value { get; } = Value;
        
        string IStringLocalStorageValue<StringSettingValue>.Serialize()
        {
            return Value;
        }
        
        StringSettingValue IStringLocalStorageValue<StringSettingValue>.Deserialize(string value)
        {
            return new StringSettingValue(value);
        }
    }
}