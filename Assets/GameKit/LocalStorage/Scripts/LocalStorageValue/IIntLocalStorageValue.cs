namespace GameKit.LocalStorage.LocalStorageValue
{
    public interface IIntLocalStorageValue <TValue> : ILocalStorageValue where TValue : ILocalStorageValue
    {
        new int Serialize();
        TValue Deserialize(int value);
        
        string ILocalStorageValue.Serialize()
        {
            return Serialize().ToString();
        }

        T ILocalStorageValue.Deserialize<T>(string value)
        {
            int parsedValue = int.Parse(value);
            return (T)(ILocalStorageValue)Deserialize(parsedValue);
        }
    }
}