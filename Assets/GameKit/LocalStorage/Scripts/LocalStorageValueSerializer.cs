using System;

namespace GameKit.LocalStorage
{
    public sealed class LocalStorageValueSerializer
    {
        public string Serialize<T>(T value) where T : ILocalStorageValue
        {
            return value.Serialize();
        }
        
        public T Deserialize<T>(string value) where T : ILocalStorageValue
        {
            var instance = CreateValueInstance<T>();
            return instance.Deserialize<T>(value);
        }

        //TODO コード生成で何とかしたい
        static ILocalStorageValue CreateValueInstance<T>() where T : ILocalStorageValue
        {
            var type = typeof(T);
            var parameterlessConstructor = type.GetConstructor(Type.EmptyTypes);
            if (parameterlessConstructor != null)
            {
                return (ILocalStorageValue)parameterlessConstructor.Invoke(null);
            }

            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
            {
                throw new MissingMethodException(type.FullName, ".ctor");
            }

            var targetConstructor = constructors[0];
            for (int i = 1; i < constructors.Length; i++)
            {
                if (constructors[i].GetParameters().Length < targetConstructor.GetParameters().Length)
                {
                    targetConstructor = constructors[i];
                }
            }

            var parameters = targetConstructor.GetParameters();
            object[] args = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                if (parameter.HasDefaultValue)
                {
                    args[i] = parameter.DefaultValue!;
                    continue;
                }

                var parameterType = parameter.ParameterType;
                args[i] = (parameterType.IsValueType ? Activator.CreateInstance(parameterType) : null)!;
            }

            return (ILocalStorageValue)targetConstructor.Invoke(args);
        }
    }
}