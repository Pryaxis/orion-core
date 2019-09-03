using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Orion.Framework.Extensions {
    /// <summary>
    /// Provides extension methods for the <see cref="Type"/> class.
    /// </summary>
    internal static class TypeExtensions {
        public static IEnumerable<Type> GetAllSubtypes(this Type baseType) {
            IEnumerable<Type> GetTypes(Assembly assembly) {
                try {
                    return assembly.GetTypes();
                } catch (ReflectionTypeLoadException ex) {
                    return ex.Types.Where(t => t != null);
                }
            }

            return AppDomain.CurrentDomain
                            .GetAssemblies()
                            .SelectMany(GetTypes)
                            .Where(t => t.IsSubclassOf(baseType))
                            .Select(GetGenericTypeMaybe);
        }

        public static Type GetGenericTypeMaybe(this Type type) {
            return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
        }
    }
}
