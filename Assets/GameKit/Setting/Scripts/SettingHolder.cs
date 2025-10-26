using System;
using System.Collections.Generic;
using GameKit.DisposableExtension;
using R3;

namespace GameKit.Setting
{
    public sealed class SettingHolder
    {
        readonly Dictionary<SettingKey, ISettingValue> settings = new();
        readonly Subject<SettingUpdateEvent> settingUpdated = new();
        
        public ReadOnlyReactiveProperty<T> GetAsReactiveProperty<T>(SettingKey key, T defaultValue, Disposer disposer) where T : ISettingValue
        {
            return settingUpdated
                .Where(e => e.Key.Equals(key))
                .Select(e => (T)e.Value)
                .ToReadOnlyReactiveProperty(Get(key, defaultValue))
                .Register(disposer);
        }
        
        public T Get<T>(SettingKey key, T defaultValue) where T : ISettingValue
        {
            if (settings.TryGetValue(key, out var value))
            {
                return (T)value;
            }

            return defaultValue;
        }
        
        public void Set<T>(SettingKey key, T value) where T : ISettingValue
        {
            if (settings.TryGetValue(key, out var oldValue))
            {
                if (oldValue is not T)
                {
                    throw new InvalidOperationException($"{key}に保存する型が一致しません。既存の型:{oldValue.GetType().FullName}, 指定された型:{typeof(T).FullName}");
                }
                settings[key] = value;
            }
            else
            {
                settings.Add(key, value);
            }

            settingUpdated.OnNext(new SettingUpdateEvent(key, value));
        }

        sealed record SettingUpdateEvent(SettingKey Key, ISettingValue Value)
        {
            public SettingKey Key { get; } = Key;
            public ISettingValue Value { get; } = Value;
        }
    }
}