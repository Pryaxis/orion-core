// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Ninject;
using Ninject.Extensions.NamedScope;

namespace Orion {
    /// <summary>
    /// Represents Orion's dependency injection container. Provides methods to manipulate <see cref="OrionPlugin"/>
    /// instances.
    /// </summary>
    [PublicAPI]
    public sealed class OrionKernel : StandardKernel {
        [NotNull, ItemNotNull]
        private static ISet<Type> InterfaceTypesToIgnore => new HashSet<Type> {
            typeof(IDisposable),
            typeof(IReadOnlyList<>),
            typeof(IService)
        };

        [NotNull, ItemNotNull] private readonly ISet<Assembly> _pluginAssemblies = new HashSet<Assembly>();
        [NotNull, ItemNotNull] private readonly ISet<Type> _pluginTypesToLoad = new HashSet<Type>();
        [NotNull, ItemNotNull] private readonly ISet<OrionPlugin> _plugins = new HashSet<OrionPlugin>();

        internal OrionKernel() {
            Bind<OrionKernel>().ToConstant(this).InSingletonScope();

            // Because we're using Assembly.Load, we'll need to have an AssemblyResolve handler to deal with any issues
            // that may pop up.
            AppDomain.CurrentDomain.AssemblyResolve +=
                (sender, args) => _pluginAssemblies.First(a => a.FullName == args.Name);
        }

        /// <summary>
        /// Gets the loaded <see cref="OrionPlugin"/> instances.
        /// </summary>
        /// <returns>The loaded <see cref="OrionPlugin"/> instances.</returns>
        public IEnumerable<OrionPlugin> GetLoadedPlugins() => new HashSet<OrionPlugin>(_plugins);

        /// <summary>
        /// Queues <see cref="OrionPlugin"/> instances to be loaded from the given assembly path.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assemblyPath"/> is <c>null</c>.</exception>
        public void QueuePluginsFromPath(string assemblyPath) {
            if (assemblyPath == null) throw new ArgumentNullException(nameof(assemblyPath));

            // Load the assembly from the path. We're using Assembly.Load with the bytes of the file so that we don't
            // hold a lock on the path, allowing the user to delete or upgrade the path.
            var assembly = Assembly.Load(File.ReadAllBytes(assemblyPath));
            _pluginAssemblies.Add(assembly);

            // Because GetExportedTypes returns closed generic types, we may potentially need to get unbound open
            // versions.
            Type GetOpenGenericTypeMaybe(Type type) => type.IsGenericType ? type.GetGenericTypeDefinition() : type;

            foreach (var serviceType in assembly.GetExportedTypes()
                                                .Where(s => s.IsSubclassOf(typeof(OrionService)))
                                                .Select(GetOpenGenericTypeMaybe)) {
                if (serviceType.IsSubclassOf(typeof(OrionPlugin))) {
                    // Bind all plugin types to themselves, allowing plugins to properly depend on other plugins
                    // without reliance on static state.
                    _pluginTypesToLoad.Add(serviceType);
                    Bind(serviceType).ToSelf().InSingletonScope();
                    continue;
                }

                var isInstanced = serviceType.GetCustomAttribute<InstancedServiceAttribute>() != null;

                // Bind all service interfaces to the implementation. We use a contextual binding here which will
                // override any default bindings that we've set in the constructor.
                foreach (var interfaceType in serviceType.GetInterfaces()
                                                         .Where(i => !InterfaceTypesToIgnore.Contains(i))
                                                         .Select(GetOpenGenericTypeMaybe)) {
                    var bind = Bind(interfaceType).To(serviceType).When(r => _pluginAssemblies.Contains(assembly));
                    if (isInstanced) {
                        bind.InParentScope();
                    } else {
                        bind.InSingletonScope();
                    }
                }
            }
        }

        /// <summary>
        /// Finishes loading <see cref="OrionPlugin"/> instances, running the given action for each plugin loaded.
        /// </summary>
        /// <param name="action">The action to run.</param>
        public void FinishLoadingPlugins(Action<OrionPlugin> action = null) {
            foreach (var pluginType in _pluginTypesToLoad.Reverse()) {
                var plugin = (OrionPlugin)this.Get(pluginType);
                _plugins.Add(plugin);
                action?.Invoke(plugin);
            }

            _pluginTypesToLoad.Clear();
        }

        /// <summary>
        /// Unloads the given <see cref="OrionPlugin"/> instance and returns a value indicating success.
        /// </summary>
        /// <param name="plugin">The <see cref="OrionPlugin"/> instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="plugin"/> is <c>null</c>.</exception>
        /// <returns>
        /// A value indicating whether the <see cref="OrionPlugin"/> instance was successfully unloaded.
        /// </returns>
        public bool UnloadPlugin(OrionPlugin plugin) {
            if (plugin == null) throw new ArgumentNullException(nameof(plugin));
            if (!_plugins.Contains(plugin)) return false;

            var pluginType = plugin.GetType();
            _pluginAssemblies.Remove(pluginType.Assembly);
            _plugins.Remove(plugin);

            plugin.Dispose();
            Unbind(pluginType);
            return true;
        }
    }
}
