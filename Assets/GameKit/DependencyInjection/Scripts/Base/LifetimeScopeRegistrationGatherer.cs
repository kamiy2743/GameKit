using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameKit.DependencyInjection.Base
{
    public sealed class LifetimeScopeRegistrationGatherer
    {
        public static IEnumerable<BaseLifetimeScopeRegistration> Get(Type parentType)
        {
            var instances = new List<BaseLifetimeScopeRegistration>();
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

                if (
                    Activator.CreateInstance(type) is BaseLifetimeScopeRegistration instance &&
                    instance.GetParentType() == parentType
                )
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

            return typeof(BaseLifetimeScopeRegistration).IsAssignableFrom(type);
        }
    }
}
