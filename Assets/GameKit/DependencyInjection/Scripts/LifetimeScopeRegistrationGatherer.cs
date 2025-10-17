using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameKit.DependencyInjection.Root
{
    public sealed class LifetimeScopeRegistrationGatherer
    {
        public static IEnumerable<ILifetimeScopeRegistration> Get()
        {
            var instances = new List<ILifetimeScopeRegistration>();
            var seen = new HashSet<Type>();

            foreach (var type in GetTypes())
            {
                if (!seen.Add(type))
                {
                    continue;
                }

                if (!IsBaseLifetimeScopeRegistration(type))
                {
                    continue;
                }

                if (Activator.CreateInstance(type) is ILifetimeScopeRegistration instance)
                {
                    instances.Add(instance);
                }
            }

            return instances;
        }

        static IEnumerable<Type> GetTypes()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in GetTypesFromAssembly(assembly))
                {
                    yield return type;
                }
            }
        }
        
        static IEnumerable<Type> GetTypesFromAssembly(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(type => type != null);
            }
        }

        static bool IsBaseLifetimeScopeRegistration(Type type)
        {
            if (type.IsAbstract)
            {
                return false;
            }

            for (var current = type; current != null; current = current.BaseType)
            {
                if (!current.IsGenericType)
                {
                    continue;
                }
                if (current.GetGenericTypeDefinition() == typeof(BaseLifetimeScopeRegistration<>))
                {
                    return true;
                }
            }

            return false;
        }
    }
}