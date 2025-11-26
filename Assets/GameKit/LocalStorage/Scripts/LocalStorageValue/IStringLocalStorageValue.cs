namespace GameKit.LocalStorage.LocalStorageValue
{
    public interface IStringLocalStorageValue <TValue> : ILocalStorageValue where TValue : ILocalStorageValue
    {
        new string Serialize();
        TValue Deserialize(string value);
        
        string ILocalStorageValue.Serialize()
        {
            return Serialize();
        }

        T ILocalStorageValue.Deserialize<T>(string value)
        {
            return (T)(ILocalStorageValue)Deserialize(value);
        }
    }
}