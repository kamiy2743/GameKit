namespace GameKit.LocalStorage
{
    public interface ILocalStorageValue
    {
        string Serialize();
        T Deserialize<T>(string value) where T : ILocalStorageValue;
    }
}