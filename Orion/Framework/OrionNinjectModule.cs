using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Ninject.Extensions.NamedScope;
using Ninject.Modules;
using Orion.Framework.Extensions;

namespace Orion.Framework {
    /// <summary>
    /// A Ninject module that scans for services and plugins in the executing assembly and plugin assemblies, binding
    /// them to the appropriate scopes.
    /// </summary>
    internal sealed class OrionNinjectModule : NinjectModule {
        private readonly string _pluginDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionNinjectModule"/> class with the specified plugin
        /// directory.
        /// </summary>
        /// <param name="pluginDirectory">The plugin directory.</param>
        /// <exception cref="ArgumentNullException"><paramref name="pluginDirectory"/> is <c>null</c>.</exception>
        internal OrionNinjectModule(string pluginDirectory) {
            _pluginDirectory = pluginDirectory ?? throw new ArgumentNullException(nameof(pluginDirectory));
        }

        /// <inheritdoc />
        public override void Load() {
            // Load assemblies in the plugin directory.
            foreach (var assemblyPath in Directory.EnumerateFiles(_pluginDirectory, "*.dll")) {
                try {
                    Assembly.LoadFrom(assemblyPath);
                } catch (Exception ex) when (ex is BadImageFormatException || ex is IOException) { }
            }

            IList<Type> overrideServiceTypes = new List<Type>();

            // Bind all non-override services.
            foreach (var serviceType in typeof(OrionService).GetAllSubtypes()) {
                var isOverride = Attribute.GetCustomAttribute(serviceType, typeof(OverrideServiceAttribute)) != null;
                if (isOverride) {
                    overrideServiceTypes.Add(serviceType);
                } else {
                    BindService(serviceType);
                }
            }

            // Bind all override services by first unbinding and then re-binding.
            foreach (var serviceType in overrideServiceTypes) {
                Unbind(serviceType);
                foreach (var interfaceType in serviceType.GetInterfaces()) {
                    Unbind(interfaceType);
                }

                BindService(serviceType);
            }
        }

        private void BindService(Type serviceType) {
            var isInstanced = Attribute.GetCustomAttribute(serviceType, typeof(InstancedServiceAttribute)) != null;
            if (isInstanced) {
                /*
                 * Bind the service in instanced (parent) scope. This way, the service will be disposed of whenever
                 * the parent object is garbage collected.
                 *
                 * Note that we need transient scope if the object is retrieved via Get<T> from the kernel. We can
                 * check this by checking whether the request target is null.
                 */

                Bind(serviceType).ToSelf().When(request => request.Target != null).InParentScope();
                Bind(serviceType).ToSelf().When(request => request.Target == null).InTransientScope();
                foreach (var interfaceType in serviceType.GetInterfaces()) {
                    Bind(interfaceType).To(serviceType).When(request => request.Target != null).InParentScope();
                    Bind(interfaceType).To(serviceType).When(request => request.Target == null).InTransientScope();
                }
            } else {
                // Bind the service in singleton scope.
                Bind(serviceType).ToSelf().InSingletonScope();
                foreach (var interfaceType in serviceType.GetInterfaces()) {
                    Bind(interfaceType).To(serviceType).InSingletonScope();
                }
            }
        }
    }
}
