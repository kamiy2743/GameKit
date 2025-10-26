using System;

namespace GameKit.Setting.SettingValue
{
    public sealed record EnumSettingValue<T>(T Value) : ISettingValue where T : Enum
    {
        public T Value { get; } = Value;
    }
}