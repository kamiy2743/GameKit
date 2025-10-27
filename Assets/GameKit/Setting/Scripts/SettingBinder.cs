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

        public void Bind<TProperty, TValue>(ISettingBindable<TValue> bindable, Disposer disposer)
            where TProperty : ISettingProperty<TValue>
            where TValue : ISettingValue
        {
            var value = settingHolder.Get<TProperty, TValue>();
            bindable.SetValue(value);

            bindable.OnValueChange()
                .Subscribe(x => settingHolder.Set<TProperty, TValue>(x))
                .AddTo(disposer);
        }
    }
}