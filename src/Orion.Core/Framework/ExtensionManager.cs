// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Ninject;
using Orion.Core.Properties;
using Serilog;

namespace Orion.Core.Framework
{
    /// <summary>
    /// Handles extension (services and plugins) initialization and cleanup.
    /// </summary>
    public sealed class ExtensionManager : IDisposable
    {
        private readonly ILogger _log;
        private readonly IKernel _kernel = new StandardKernel();

        private readonly ISet<Type> _serviceInterfaceTypes = new HashSet<Type>();
        private readonly IDictionary<Type, ISet<Type>> _serviceBindingTypes = new Dictionary<Type, ISet<Type>>();
        private readonly ISet<Type> _pluginTypes = new HashSet<Type>();

        private readonly Dictionary<string, OrionPlugin> _plugins = new Dictionary<string, OrionPlugin>();

        internal ExtensionManager(OrionKernel kernel, ILogger log)
        {
            Debug.Assert(kernel != null);
            Debug.Assert(log != null);

            _log = log;

            // Bind `OrionKernel` to `kernel` so that extensions retrieve `kernel`.
            _kernel.Bind<OrionKernel>().ToConstant(kernel);

            // Bind `Ilogger` so that extensions retrieve extension-specific logs.
            _kernel
                .Bind<ILogger>()
                .ToMethod(ctx =>
                {
                    var type = ctx.Request.Target.Member.ReflectedType;
                    Debug.Assert(type != null);

                    var name =
                        type.GetCustomAttribute<BindingAttribute>()?.Name ??
                        type.GetCustomAttribute<PluginAttribute>()!.Name;
                    return log.ForContext("Name", name);
                })
                .InTransientScope();
        }

        /// <summary>
        /// Gets a mapping from plugin names to currently loaded plugins.
        /// </summary>
        /// <value>A mapping from plugin names to plugins.</value>
        public IReadOnlyDictionary<string, OrionPlugin> Plugins => _plugins;

        /// <inheritdoc/>
        public void Dispose()
        {
            _kernel.Dispose();
        }

        /// <summary>
        /// Loads service definitions, service bindings, and plugins from the given <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly">The assembly to load from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <see langword="null"/>.</exception>
        public void Load(Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            LoadServiceInterfaceTypes(assembly);
            LoadServiceBindingTypes(assembly);
            LoadPluginTypes(assembly);
        }

        /// <summary>
        /// Initializes the loaded service bindings and plugins.
        /// </summary>
        public void Initialize()
        {
            InitializeServiceBindings();
            InitializePlugins();

            // Clear out the loaded types so that a second `Initialize` call won't perform redundant initialization
            // logic.
            _serviceInterfaceTypes.Clear();
            _serviceBindingTypes.Clear();
            _pluginTypes.Clear();
        }
        
        // TODO: implement a method to unload an assembly.

        private void LoadServiceInterfaceTypes(Assembly assembly)
        {
            Debug.Assert(assembly != null);

            _serviceInterfaceTypes.UnionWith(
                assembly.ExportedTypes
                    .Where(t => t.IsInterface)
                    .Where(t => t.GetCustomAttribute<ServiceAttribute>() != null));
        }

        private void LoadServiceBindingTypes(Assembly assembly)
        {
            Debug.Assert(assembly != null);

            foreach (var bindingType in assembly.DefinedTypes
                .Where(t => !t.IsAbstract)
                .Where(t => t.GetCustomAttribute<BindingAttribute>() != null))
            {
                foreach (var interfaceType in bindingType
                    .GetInterfaces()
                    .Where(_serviceInterfaceTypes.Contains))
                {
                    if (!_serviceBindingTypes.TryGetValue(interfaceType, out var types))
                    {
                        types = new HashSet<Type>();
                        _serviceBindingTypes[interfaceType] = types;
                    }

                    types.Add(bindingType);
                }
            }
        }

        private void LoadPluginTypes(Assembly assembly)
        {
            Debug.Assert(assembly != null);

            foreach (var pluginType in assembly.ExportedTypes
                .Where(t => !t.IsAbstract)
                .Where(t => t.GetCustomAttribute<PluginAttribute>() != null))
            {
                _pluginTypes.Add(pluginType);

                var pluginName = pluginType.GetCustomAttribute<PluginAttribute>()!.Name;
                _log.Information(Resources.Kernel_LoadedPlugin, pluginName);
            }
        }

        private void InitializeServiceBindings()
        {
            foreach (var (interfaceType, bindingTypes) in _serviceBindingTypes)
            {
                var bindingType = bindingTypes
                    .OrderByDescending(t => t.GetCustomAttribute<BindingAttribute>()!.Priority)
                    .FirstOrDefault();
                if (bindingType is null)
                {
                    // No binding found for `interfaceType`, continue.
                    continue;
                }

                var binding = _kernel.Bind(interfaceType).To(bindingType);

                var scope = interfaceType.GetCustomAttribute<ServiceAttribute>()!.Scope;
                _ = scope switch
                {
                    ServiceScope.Singleton => binding.InSingletonScope(),
                    ServiceScope.Transient => binding.InTransientScope(),

                    // Not localized because this string is developer-facing.
                    _ => throw new InvalidOperationException("Invalid service scope")
                };

                // Eagerly request singleton-scoped services. That way, an instance always exists.
                if (scope == ServiceScope.Singleton)
                {
                    _ = _kernel.Get(interfaceType);
                }
            }
        }

        private void InitializePlugins()
        {
            // Initialize the plugin bindings to allow plugin dependencies.
            foreach (var pluginType in _pluginTypes)
            {
                _kernel.Bind(pluginType).ToSelf().InSingletonScope();
            }

            // Initialize the plugins.
            foreach (var pluginType in _pluginTypes)
            {
                var plugin = (OrionPlugin)_kernel.Get(pluginType);
                plugin.Initialize();

                var attribute = pluginType.GetCustomAttribute<PluginAttribute>();
                var pluginName = attribute.Name;
                _plugins[pluginName] = plugin;

                var pluginVersion = pluginType.Assembly.GetName().Version;
                var pluginAuthor = attribute.Author;
                _log.Information(Resources.Kernel_InitializedPlugin, pluginName, pluginVersion, pluginAuthor);
            }
        }
    }
}
