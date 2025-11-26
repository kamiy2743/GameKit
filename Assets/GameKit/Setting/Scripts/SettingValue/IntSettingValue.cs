using System.Globalization;
using GameKit.LocalStorage;

namespace GameKit.Setting.SettingValue
{
    public sealed record IntSettingValue(int Value) : ISettingValue
    {
        public int Value { get; } = Value;
        
        string ILocalStorageValue.Serialize()
        {
            return Value.ToString();
        }

        T ILocalStorageValue.Deserialize<T>(string value)
        {
            int parsedValue = int.Parse(value, CultureInfo.CurrentCulture);
            return (T)(ILocalStorageValue)new IntSettingValue(parsedValue);
        }
    }
}