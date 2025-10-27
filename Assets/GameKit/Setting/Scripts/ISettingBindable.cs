using R3;

namespace GameKit.Setting
{
    public interface ISettingBindable<T> where T : ISettingValue
    {
        void SetValue(T value);
        Observable<T> OnValueChange();
    }
}