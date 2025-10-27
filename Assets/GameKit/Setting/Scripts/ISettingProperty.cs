namespace GameKit.Setting
{
    public interface ISettingProperty<T> where T : ISettingValue
    {
        T Default { get; }
    }
}