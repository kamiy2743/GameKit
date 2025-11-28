using System;
using GameKit.DisposableExtension;
using GameKit.LocalStorage;
using R3;

namespace GameKit.Setting
{
    public sealed class SettingHolder
    {
        const string LocalStorageKeyCategory = "Setting";
        
        readonly LocalStorage.LocalStorage localStorage;

        readonly Subject<LocalStorageKey> settingUpdated = new();

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
                .Where(k => k.Equals(key))
                .Select(_ => Get<TProperty, TValue>(key))
                .ToReadOnlyReactiveProperty(Get<TProperty, TValue>())
                .RegisterAndReturn(disposer);
        }
        
        public TValue Get<TProperty, TValue>()
            where TProperty : ISettingProperty<TValue>
            where TValue : ISettingValue
        {
            var key = GetKey<TProperty, TValue>();
            return Get<TProperty, TValue>(key);
        }
        
        TValue Get<TProperty, TValue>(LocalStorageKey key)
            where TProperty : ISettingProperty<TValue>
            where TValue : ISettingValue
        {
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
            settingUpdated.OnNext(key);
        }
        
        static LocalStorageKey GetKey<TProperty, TValue>()
            where TProperty : ISettingProperty<TValue>
            where TValue : ISettingValue
        {
            var value = typeof(TProperty).FullName!;
            return new LocalStorageKey(value, LocalStorageKeyCategory);
        }

        public void Reset()
        {
            var keys = localStorage.GetKeys(LocalStorageKeyCategory);
            localStorage.DeleteKeys(LocalStorageKeyCategory);

            foreach (var key in keys)
            {
                settingUpdated.OnNext(key);
            }
        }
    }
}