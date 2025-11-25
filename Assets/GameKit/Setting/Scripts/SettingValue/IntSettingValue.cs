using System.Globalization;
using GameKit.LocalStorage;

namespace GameKit.Setting.SettingValue
{
    public sealed record IntSettingValue(int Value) : ISettingValue
    {
        public int Value { get; } = Value;
        
        string ILocalStorageValue.ToStringValue()
        {
            return Value.ToString();
        }

        T ILocalStorageValue.FromStringValue<T>(string value)
        {
            int parsedValue = int.Parse(value, CultureInfo.CurrentCulture);
            return (T)(ILocalStorageValue)new IntSettingValue(parsedValue);
        }
    }
}