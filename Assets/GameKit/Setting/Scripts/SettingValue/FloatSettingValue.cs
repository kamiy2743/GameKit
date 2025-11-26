using GameKit.LocalStorage.LocalStorageValue;

namespace GameKit.Setting.SettingValue
{
    public sealed record FloatSettingValue(float Value) : ISettingValue, IFloatLocalStorageValue<FloatSettingValue>
    {
        public float Value { get; } = Value;
        
        float IFloatLocalStorageValue<FloatSettingValue>.Serialize()
        {
            return Value;
        }
        
        FloatSettingValue IFloatLocalStorageValue<FloatSettingValue>.Deserialize(float value)
        {
            return new FloatSettingValue(value);
        }
    }
}