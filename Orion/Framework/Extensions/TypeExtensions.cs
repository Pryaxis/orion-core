namespace Orion.Framework.Extensions {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Provides extension methods for the <see cref="Type"/> class.
    /// </summary>
    internal static class TypeExtensions {
        /// <summary>
        /// Gets all subtypes of the specified type within the current <see cref="AppDomain"/>.
        /// </summary>
        /// <param name="baseType">The type.</param>
        /// <returns>All of the subtypes.</returns>
        public static IEnumerable<Type> GetAllSubtypes(this Type baseType) {
            IEnumerable<Type> GetTypes(Assembly assembly) {
                try {
                    return assembly.GetTypes();
                } catch (ReflectionTypeLoadException ex) {
                    return ex.Types.Where(t => t != null);
                }
            }

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(GetTypes)
                .Where(t => t.IsSubclassOf(baseType))
                .Select(GetGenericTypeMaybe);
        }

        /// <summary>
        /// Returns either the specified type or its genericized version, if applicable.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The type or its genericized version, if applicable.</returns>
        public static Type GetGenericTypeMaybe(this Type type) {
            return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
        }
    }
}
