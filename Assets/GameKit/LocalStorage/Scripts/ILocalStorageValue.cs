namespace GameKit.LocalStorage
{
    public interface ILocalStorageValue
    {
        string ToStringValue();
        T FromStringValue<T>(string value) where T : ILocalStorageValue;
    }
}