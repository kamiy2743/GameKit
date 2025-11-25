using System;
using GameKit.DisposableExtension;
using R3;

namespace GameKit.Setting
{
    public sealed class SettingHolder
    {
        readonly LocalStorage.LocalStorage localStorage;

        readonly Subject<SettingUpdateEvent> settingUpdated = new();

        public SettingHolder(LocalStorage.LocalStorage localStorage)
        {
            this.localStorage = localStorage;
        }
        
        public ReadOnlyReactiveProperty<TValue> GetAsReactiveProperty<TProperty, TValue>(Disposer disposer)
            where TProperty : ISettingProperty<TValue>
            where TValue : ISettingValue
        {
            var key = GetKey<TProperty, TValue>();
            return settingUpdated
                .Where(e => e.Key.Equals(key))
                .Select(e => (TValue)e.Value)
                .ToReadOnlyReactiveProperty(Get<TProperty, TValue>())
                .RegisterAndReturn(disposer);
        }
        
        public TValue Get<TProperty, TValue>()
            where TProperty : ISettingProperty<TValue>
            where TValue : ISettingValue
        {
            var key = GetKey<TProperty, TValue>();
            if (localStorage.TryGet<TValue>(key, out var value))
            {
                return value;
            }

            var property = Activator.CreateInstance<TProperty>();
            localStorage.Insert(key, property.Default);
            return property.Default;
        }
        
        public void Set<TProperty, TValue>(TValue value)
            where TProperty : ISettingProperty<TValue>
            where TValue : ISettingValue
        {
            var key = GetKey<TProperty, TValue>();
            localStorage.InsertOrUpdate(key, value);
            settingUpdated.OnNext(new SettingUpdateEvent(key, value));
        }
        
        static string GetKey<TProperty, TValue>()
            where TProperty : ISettingProperty<TValue>
            where TValue : ISettingValue
        {
            return typeof(TProperty).FullName!;
        }

        sealed record SettingUpdateEvent(string Key, ISettingValue Value)
        {
            public string Key { get; } = Key;
            public ISettingValue Value { get; } = Value;
        }
    }
}