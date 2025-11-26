using GameKit.LocalStorage.LocalStorageValue;

namespace GameKit.Setting.SettingValue
{
    public sealed record IntSettingValue(int Value) : ISettingValue, IIntLocalStorageValue<IntSettingValue>
    {
        public int Value { get; } = Value;
        
        int IIntLocalStorageValue<IntSettingValue>.Serialize()
        {
            return Value;
        }
        
        IntSettingValue IIntLocalStorageValue<IntSettingValue>.Deserialize(int value)
        {
            return new IntSettingValue(value);
        }
    }
}