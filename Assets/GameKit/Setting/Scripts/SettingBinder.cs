using GameKit.DisposableExtension;
using R3;

namespace GameKit.Setting
{
    public sealed class SettingBinder
    {
        readonly SettingHolder settingHolder;

        public SettingBinder(SettingHolder settingHolder)
        {
            this.settingHolder = settingHolder;
        }

        public void Bind<T>(
            SettingKey key,
            T defaultValue,
            ISettingBindable<T> bindable,
            Disposer disposer
        ) where T : ISettingValue
        {
            var value = settingHolder.Get(key, defaultValue);
            bindable.SetValue(value);

            bindable.OnValueChange()
                .Subscribe(x => settingHolder.Set(key, x))
                .AddTo(disposer);
        }
    }
}