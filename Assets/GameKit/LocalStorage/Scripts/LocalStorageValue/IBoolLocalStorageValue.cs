namespace GameKit.LocalStorage.LocalStorageValue
{
    public interface IBoolLocalStorageValue<TValue> : ILocalStorageValue where TValue : ILocalStorageValue
    {
        new bool Serialize();
        TValue Deserialize(bool value);
        
        string ILocalStorageValue.Serialize()
        {
            return Serialize().ToString();
        }

        T ILocalStorageValue.Deserialize<T>(string value)
        {
            bool parsedValue = bool.Parse(value);
            return (T)(ILocalStorageValue)Deserialize(parsedValue);
        }
    }
}