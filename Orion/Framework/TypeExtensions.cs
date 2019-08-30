namespace Orion.Framework {
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
        /// <exception cref="ArgumentNullException"><paramref name="baseType"/> is <c>null</c>.</exception>
        public static IEnumerable<Type> GetAllSubtypes(this Type baseType) {
            if (baseType == null) {
                throw new ArgumentNullException(nameof(baseType));
            }

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
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        public static Type GetGenericTypeMaybe(this Type type) {
            if (type == null) {
                throw new ArgumentNullException(nameof(type));
            }

            return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
        }

        /// <summary>
        /// Gets all interfaces that the specified type implements.
        /// </summary>
        /// <param name="derivedType">The type.</param>
        /// <returns>All of the interfaces.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="derivedType"/> is <c>null</c>.</exception>
        public static IEnumerable<Type> GetInterfaces(this Type derivedType) {
            if (derivedType == null) {
                throw new ArgumentNullException(nameof(derivedType));
            }

            return derivedType
                .GetInterfaces()
                .Select(GetGenericTypeMaybe);
        }
    }
}
