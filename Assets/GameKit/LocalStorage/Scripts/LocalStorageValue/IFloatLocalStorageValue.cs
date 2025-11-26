using System.Globalization;

namespace GameKit.LocalStorage.LocalStorageValue
{
    public interface IFloatLocalStorageValue <TValue> : ILocalStorageValue where TValue : ILocalStorageValue
    {
        new float Serialize();
        TValue Deserialize(float value);
        
        string ILocalStorageValue.Serialize()
        {
            return Serialize().ToString(CultureInfo.CurrentCulture);
        }

        T ILocalStorageValue.Deserialize<T>(string value)
        {
            float parsedValue = float.Parse(value);
            return (T)(ILocalStorageValue)Deserialize(parsedValue);
        }
    }
}