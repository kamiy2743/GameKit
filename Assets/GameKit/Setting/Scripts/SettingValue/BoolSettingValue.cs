using GameKit.LocalStorage.LocalStorageValue;

namespace GameKit.Setting.SettingValue
{
    public sealed record BoolSettingValue(bool Value) : ISettingValue, IBoolLocalStorageValue<BoolSettingValue>
    {
        public bool Value { get; } = Value;

        bool IBoolLocalStorageValue<BoolSettingValue>.Serialize()
        {
            return Value;
        }

        BoolSettingValue IBoolLocalStorageValue<BoolSettingValue>.Deserialize(bool value)
        {
            return new BoolSettingValue(value);
        }
    }
}