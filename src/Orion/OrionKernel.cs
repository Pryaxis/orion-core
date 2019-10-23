// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Ninject;
using Orion.Events;
using Orion.Events.Server;
using Orion.Items;
using Orion.Npcs;
using Orion.Players;
using Orion.Projectiles;
using Orion.Properties;
using Orion.World;
using Orion.World.TileEntities;
using OTAPI;
using Serilog;

namespace Orion {
    /// <summary>
    /// Represents Orion's dependency injection container. Provides methods to manipulate <see cref="OrionPlugin"/>
    /// instances and server-related events. This class is not thread-safe.
    /// </summary>
    /// <remarks>The <see cref="OrionKernel"/> class is responsible for managing Orion's plugins and services.</remarks>
    public sealed class OrionKernel : StandardKernel {
        private readonly ILogger _log;

        private readonly ISet<Assembly> _pluginAssemblies = new HashSet<Assembly>();
        private readonly ISet<Type> _pluginTypesToLoad = new HashSet<Type>();
        private readonly Dictionary<string, OrionPlugin> _plugins = new Dictionary<string, OrionPlugin>();
        private readonly Dictionary<OrionPlugin, string> _pluginToName = new Dictionary<OrionPlugin, string>();

        private readonly MethodInfo _registerHandler = typeof(OrionKernel).GetMethod(nameof(RegisterHandler));
        private readonly MethodInfo _unregisterHandler = typeof(OrionKernel).GetMethod(nameof(UnregisterHandler));
        private readonly IDictionary<Type, object> _eventHandlerCollections = new Dictionary<Type, object>();

        private readonly IDictionary<object, HashSet<(Type type, object handler)>> _objectToHandlers =
            new Dictionary<object, HashSet<(Type type, object handler)>>();

        /// <summary>
        /// Gets a read-only mapping from plugin names to <see cref="OrionPlugin"/> instances.
        /// </summary>
        /// <value>A read-only mapping from plugin names to plugins.</value>
        public IReadOnlyDictionary<string, OrionPlugin> Plugins => _plugins;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionKernel"/> class with the specified log.
        /// </summary>
        /// <param name="log">The log. This log will be used to create all injected logs.</param>
        /// <exception cref="ArgumentNullException"><paramref name="log"/> is <see langword="null"/>.</exception>
        public OrionKernel(ILogger log) {
            if (log is null) {
                throw new ArgumentNullException(nameof(log));
            }

            _log = log.ForContext("ServiceName", "orion-kernel");

            Bind<OrionKernel>().ToConstant(this).InSingletonScope();
            Bind<IItemService>().To<OrionItemService>().InSingletonScope();
            Bind<INpcService>().To<OrionNpcService>().InSingletonScope();
            Bind<IPlayerService>().To<OrionPlayerService>().InSingletonScope();
            Bind<IProjectileService>().To<OrionProjectileService>().InSingletonScope();
            Bind<ITileEntityService>().To<OrionTileEntityService>().InSingletonScope();
            Bind<IWorldService>().To<OrionWorldService>().InSingletonScope();

            // Create an ILogger binding for service-specific logs.
            Bind<ILogger>()
                .ToMethod(ctx => {
                    // ctx.Request.Target can be null if the ILogger is requested directly.
                    var type = ctx.Request.Target?.Member.ReflectedType;
                    var serviceName = type?.GetCustomAttribute<ServiceAttribute>()?.Name ?? type?.Name ?? string.Empty;
                    return log.ForContext("ServiceName", serviceName);
                })
                .InTransientScope();

            // Create bindings for Lazy<T> for lazily-loaded services.
            var getLazy = GetType().GetMethod(nameof(GetLazy), BindingFlags.NonPublic | BindingFlags.Instance);
            Bind(typeof(Lazy<>))
                .ToMethod(ctx => getLazy
                    .MakeGenericMethod(ctx.GenericArguments[0])
                    .Invoke(this, Array.Empty<object>()))
                .InTransientScope();

            // Because we're using Assembly.Load, we'll need to have an AssemblyResolve handler to deal with assembly
            // resolution issues.
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolveHandler;

            Hooks.Game.PreInitialize += PreInitializeHandler;
            Hooks.Game.Started += StartedHandler;
            Hooks.Game.PreUpdate += PreUpdateHandler;
            Hooks.Command.Process += ProcessHandler;
        }

        /// <summary>
        /// Disposes the kernel, releasing any resources associated with it.
        /// </summary>
        /// <param name="disposing">
        /// <see langword="true"/> to dispose all resources; <see langword="false"/> to dispose only unmanaged
        /// resources.
        /// </param>
        public override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (!disposing) {
                return;
            }

            AppDomain.CurrentDomain.AssemblyResolve -= AssemblyResolveHandler;

            Hooks.Game.PreInitialize -= PreInitializeHandler;
            Hooks.Game.Started -= StartedHandler;
            Hooks.Game.PreUpdate -= PreUpdateHandler;
            Hooks.Command.Process -= ProcessHandler;
        }

        /// <summary>
        /// Starts loading plugins from an <paramref name="assemblyPath"/>.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <remarks>
        /// The reason that loading plugins needs to be split up into a two-part process is that the plugin types need
        /// to be collected before actually constructing them due to dependency injection requirements.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="assemblyPath"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="BadImageFormatException">The file is not a valid assembly.</exception>
        public void StartLoadingPlugins(string assemblyPath) {
            if (assemblyPath is null) {
                throw new ArgumentNullException(nameof(assemblyPath));
            }

            // Load the assembly from the path. We're using Assembly.Load with the bytes of the file so that we don't
            // hold a lock on the path, allowing the user to delete or upgrade the path.
            var assembly = Assembly.Load(File.ReadAllBytes(assemblyPath));
            _pluginAssemblies.Add(assembly);

            foreach (var pluginType in assembly.ExportedTypes.Where(s => s.IsSubclassOf(typeof(OrionPlugin)))) {
                // Bind all plugin types to themselves, allowing plugins to depend on other plugins.
                _pluginTypesToLoad.Add(pluginType);
                Bind(pluginType).ToSelf().InSingletonScope();

                var pluginName = pluginType.GetCustomAttribute<ServiceAttribute?>()?.Name ?? pluginType.Name;
                _log.Information(Resources.Kernel_LoadPlugin, pluginName, assemblyPath);
            }
        }

        /// <summary>
        /// Finishes loading plugins and returns the initialized <see cref="OrionPlugin"/> instances.
        /// </summary>
        /// <returns>The initialized <see cref="OrionPlugin"/> instances.</returns>
        /// <remarks>Each plugin will be constructed and then each plugin will be initialized.</remarks>
        public IReadOnlyCollection<OrionPlugin> FinishLoadingPlugins() {
            OrionPlugin LoadPlugin(Type pluginType) {
                var plugin = (OrionPlugin)this.Get(pluginType);
                var pluginName = pluginType.GetCustomAttribute<ServiceAttribute?>()?.Name ?? pluginType.Name;
                _plugins[pluginName] = plugin;
                _pluginToName[plugin] = pluginName;
                return plugin;
            }

            // We need to first construct all the plugins before initializing the plugins. This allows the bindings that
            // occur in the constructors to take effect in the initialization.
            var loadedPlugins = _pluginTypesToLoad.Select(LoadPlugin).ToList();
            foreach (var plugin in loadedPlugins) {
                plugin.Initialize();

                var pluginType = plugin.GetType();
                var pluginName = _pluginToName[plugin];
                var pluginVersion = pluginType.Assembly.GetName().Version;
                var pluginAuthor = pluginType.GetCustomAttribute<ServiceAttribute?>()?.Author ?? "Pryaxis";
                _log.Information(Resources.Kernel_InitalizePlugin, pluginName, pluginVersion, pluginAuthor);
            }

            _pluginTypesToLoad.Clear();
            return loadedPlugins;
        }

        /// <summary>
        /// Unloads a <paramref name="plugin"/> and returns a value indicating success.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <returns><see langword="true"/> if the plugin was unloaded; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// This method can potentially corrupt the state of <see cref="OrionPlugin"/> instances that depend on
        /// <paramref name="plugin"/> and hence should be used very carefully.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="plugin"/> is <see langword="null"/>.</exception>
        public bool UnloadPlugin(OrionPlugin plugin) {
            if (plugin is null) {
                throw new ArgumentNullException(nameof(plugin));
            }

            if (!_pluginToName.TryGetValue(plugin, out var pluginName)) {
                return false;
            }

            var pluginType = plugin.GetType();
            _pluginAssemblies.Remove(pluginType.Assembly);
            _plugins.Remove(pluginName);
            _pluginToName.Remove(plugin);
            plugin.Dispose();
            Unbind(pluginType);

            _log.Information(Resources.Kernel_UnloadPlugin, pluginName);
            return true;
        }

        /// <summary>
        /// Registers the given <paramref name="handler"/> as an event handler for the relevant event, logging to the
        /// specified <paramref name="log"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="handler">The handler.</param>
        /// <param name="log">The log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handler"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        public void RegisterHandler<TEvent>(Action<TEvent> handler, ILogger log) where TEvent : Event {
            if (handler is null) {
                throw new ArgumentNullException(nameof(handler));
            }

            if (log is null) {
                throw new ArgumentNullException(nameof(log));
            }

            var collection = GetEventHandlerCollection<TEvent>();
            collection.RegisterHandler(handler, log);
        }

        /// <summary>
        /// Registers all of the methods marked with <see cref="EventHandlerAttribute"/> as handlers in the given
        /// <paramref name="handlerObject"/>, logging to the specified <paramref name="log"/>.
        /// </summary>
        /// <param name="handlerObject">The handler object.</param>
        /// <param name="log">The log.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="handlerObject"/> contains a method marked with <see cref="EventHandlerAttribute"/> which is
        /// not of the correct form.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handlerObject"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        public void RegisterHandlers(object handlerObject, ILogger log) {
            if (handlerObject is null) {
                throw new ArgumentNullException(nameof(handlerObject));
            }

            if (log is null) {
                throw new ArgumentNullException(nameof(log));
            }

            var set = _objectToHandlers[handlerObject] = new HashSet<(Type type, object handler)>();
            foreach (var method in handlerObject.GetType().GetMethods()) {
                if (method.GetCustomAttribute<EventHandlerAttribute>() is null) {
                    continue;
                }

                var parameters = method.GetParameters();
                if (parameters.Length != 1) {
                    // Not localized because this string is developer-facing.
                    throw new ArgumentException(
                        $"Method {method.Name} does not have exactly one argument.", nameof(handlerObject));
                }

                var type = parameters[0].ParameterType;
                if (!type.IsSubclassOf(typeof(Event))) {
                    // Not localized because this string is developer-facing.
                    throw new ArgumentException(
                        $"Method {method.Name} does not have an argument of type {nameof(Event)}.",
                        nameof(handlerObject));
                }

                var handlerType = typeof(Action<>).MakeGenericType(type);
                var handler = method.CreateDelegate(handlerType, handlerObject);
                _registerHandler.MakeGenericMethod(type).Invoke(this, new object?[] { handler, log });
                set.Add((type, handler));
            }
        }

        /// <summary>
        /// Raises the given event, logging to the specified <paramref name="log"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="e">The event.</param>
        /// <param name="log">The log.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="e"/> or <paramref name="log"/> is <see langword="null"/>.
        /// </exception>
        [SuppressMessage("Design", "CA1030:Use events where appropriate", Justification = "Can't use .NET events")]
        public void RaiseEvent<TEvent>(TEvent e, ILogger log) where TEvent : Event {
            if (e is null) {
                throw new ArgumentNullException(nameof(e));
            }

            if (log is null) {
                throw new ArgumentNullException(nameof(log));
            }

            var collection = GetEventHandlerCollection<TEvent>();
            collection.Raise(e, log);
        }

        /// <summary>
        /// Unregisters the given <paramref name="handler"/>, logging to the specified <paramref name="log"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="handler">The handler.</param>
        /// <param name="log">The log.</param>
        /// <remarks>
        /// Handlers should be unregistered where possible. Neglecting to do so can result in memory locks and incorrect
        /// cleanup.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handler"/> or <paramref name="log"/> is <see langword="null"/>.
        /// </exception>
        public void UnregisterHandler<TEvent>(Action<TEvent> handler, ILogger log) where TEvent : Event {
            if (handler is null) {
                throw new ArgumentNullException(nameof(handler));
            }

            if (log is null) {
                throw new ArgumentNullException(nameof(log));
            }

            var collection = GetEventHandlerCollection<TEvent>();
            collection.UnregisterHandler(handler, log);
        }

        /// <summary>
        /// Unregisters all of the methods marked with <see cref="EventHandlerAttribute"/> as handlers in the given
        /// <paramref name="handlerObject"/>, logging to the specified <paramref name="log"/>
        /// </summary>
        /// <param name="handlerObject">The handler object.</param>
        /// <param name="log">The log.</param>
        /// <remarks>
        /// Handlers should be unregistered where possible. Neglecting to do so can result in memory locks and incorrect
        /// cleanup.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handlerObject"/> or <paramref name="log"/> is <see langword="null"/>.
        /// </exception>
        public void UnregisterHandlers(object handlerObject, ILogger log) {
            if (handlerObject is null) {
                throw new ArgumentNullException(nameof(handlerObject));
            }

            if (log is null) {
                throw new ArgumentNullException(nameof(log));
            }

            if (!_objectToHandlers.TryGetValue(handlerObject, out var set)) {
                return;
            }

            _objectToHandlers.Remove(handlerObject);
            foreach (var (type, handler) in set) {
                _unregisterHandler.MakeGenericMethod(type).Invoke(this, new object[] { handler, log });
            }
        }

        private Lazy<T> GetLazy<T>() => new Lazy<T>(() => this.Get<T>());

        private Assembly AssemblyResolveHandler(object sender, ResolveEventArgs e) =>
            _pluginAssemblies.FirstOrDefault(a => a.FullName == e.Name);

        private EventHandlerCollection<TEvent> GetEventHandlerCollection<TEvent>() where TEvent : Event {
            var type = typeof(TEvent);
            if (!_eventHandlerCollections.TryGetValue(type, out var collection)) {
                collection = new EventHandlerCollection<TEvent>();
                _eventHandlerCollections[type] = collection;
            }

            return (EventHandlerCollection<TEvent>)collection;
        }

        private void PreInitializeHandler() {
            var e = new ServerInitializeEvent();
            RaiseEvent(e, _log);
        }

        private void StartedHandler() {
            var e = new ServerStartEvent();
            RaiseEvent(e, _log);
        }

        private void PreUpdateHandler(ref GameTime _) {
            var e = new ServerUpdateEvent();
            RaiseEvent(e, _log);
        }

        private HookResult ProcessHandler(string _, string input) {
            var e = new ServerCommandEvent(input);
            RaiseEvent(e, _log);
            return e.IsCanceled() ? HookResult.Cancel : HookResult.Continue;
        }
    }
}
