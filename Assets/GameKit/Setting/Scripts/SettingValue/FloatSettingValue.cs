using System.Globalization;
using GameKit.LocalStorage;

namespace GameKit.Setting.SettingValue
{
    public sealed record FloatSettingValue(float Value) : ISettingValue
    {
        public float Value { get; } = Value;

        string ILocalStorageValue.Serialize()
        {
            return Value.ToString(CultureInfo.CurrentCulture);
        }

        T ILocalStorageValue.Deserialize<T>(string value)
        {
            float parsedValue = float.Parse(value, CultureInfo.CurrentCulture);
            return (T)(ILocalStorageValue)new FloatSettingValue(parsedValue);
        }
    }
}