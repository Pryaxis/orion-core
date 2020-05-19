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
using System.Linq;
using System.Reflection;
using Ninject;
using Orion.Events;
using Orion.Events.Server;
using Orion.Properties;
using Serilog;

namespace Orion {
    /// <summary>
    /// Represents Orion's core logic. Provides methods to manipulate Orion plugins and events.
    /// </summary>
    public sealed class OrionKernel : IDisposable {
        private readonly ILogger _log;

        private readonly MethodInfo _getLazy =
            typeof(OrionKernel).GetMethod(nameof(GetLazy), BindingFlags.NonPublic | BindingFlags.Instance);

        private readonly IList<Type> _pluginTypesToLoad = new List<Type>();
        private readonly Dictionary<string, OrionPlugin> _plugins = new Dictionary<string, OrionPlugin>();

        private readonly MethodInfo _registerHandler = typeof(OrionKernel).GetMethod(nameof(RegisterHandler));
        private readonly MethodInfo _deregisterHandler = typeof(OrionKernel).GetMethod(nameof(DeregisterHandler));
        private readonly IDictionary<Type, object> _eventHandlerCollections = new Dictionary<Type, object>();
        private readonly IDictionary<object, IList<(Type eventType, object handler)>> _handlerRegistrations =
            new Dictionary<object, IList<(Type eventType, object handler)>>();

        /// <summary>
        /// Gets the dependency injection container.
        /// </summary>
        /// <value>The dependency injection container.</value>
        public IKernel Container { get; } = new StandardKernel();

        /// <summary>
        /// Gets a read-only mapping from plugin names to plugins.
        /// </summary>
        /// <value>A read-only mapping from plugin names to plugins.</value>
        public IReadOnlyDictionary<string, OrionPlugin> Plugins => _plugins;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionKernel"/> class with the specified <paramref name="log"/>.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <exception cref="ArgumentNullException"><paramref name="log"/> is <see langword="null"/>.</exception>
        public OrionKernel(ILogger log) {
            if (log is null) {
                throw new ArgumentNullException(nameof(log));
            }

            _log = log.ForContext("ServiceName", "orion-kernel");

            Container.Bind<OrionKernel>().ToConstant(this).InSingletonScope();

            // Create an ILogger binding for service-specific logs.
            Container.Bind<ILogger>()
                .ToMethod(ctx => {
                    // ctx.Request.Target can be null if the ILogger is requested directly.
                    var type = ctx.Request.Target?.Member.ReflectedType;
                    var serviceName = type?.GetCustomAttribute<ServiceAttribute>()?.Name ?? type?.Name ?? string.Empty;
                    return log.ForContext("ServiceName", serviceName);
                })
                .InTransientScope();

            // Create a Lazy<T> binding for lazily-loaded services.
            Container.Bind(typeof(Lazy<>))
                .ToMethod(ctx => _getLazy
                    .MakeGenericMethod(ctx.GenericArguments[0])
                    .Invoke(this, Array.Empty<object>()))
                .InTransientScope();

            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolveHandler;

            OTAPI.Hooks.Game.PreInitialize += PreInitializeHandler;
            OTAPI.Hooks.Game.PostInitialize += PostInitializeHandler;
            OTAPI.Hooks.Game.Started += StartedHandler;
            OTAPI.Hooks.Command.Process += ProcessHandler;
        }

        /// <summary>
        /// Disposes the kernel, releasing any resources associated with it.
        /// </summary>
        public void Dispose() {
            Container.Dispose();

            AppDomain.CurrentDomain.AssemblyResolve -= AssemblyResolveHandler;

            OTAPI.Hooks.Game.PreInitialize -= PreInitializeHandler;
            OTAPI.Hooks.Game.PostInitialize -= PostInitializeHandler;
            OTAPI.Hooks.Game.Started -= StartedHandler;
            OTAPI.Hooks.Command.Process -= ProcessHandler;
        }

        /// <summary>
        /// Loads plugins from the given <paramref name="assembly"/>.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assembly"/> is <see langword="null"/>.</exception>
        public void LoadPlugins(Assembly assembly) {
            if (assembly is null) {
                throw new ArgumentNullException(nameof(assembly));
            }

            foreach (var pluginType in assembly.ExportedTypes.Where(t => t.IsSubclassOf(typeof(OrionPlugin)))) {
                _pluginTypesToLoad.Add(pluginType);

                // Bind all plugin types to themselves, allowing plugins to depend on other plugins.
                Container.Bind(pluginType).ToSelf().InSingletonScope();

                var pluginName = pluginType.GetCustomAttribute<ServiceAttribute?>()?.Name ?? pluginType.Name;
                _log.Information(Resources.Kernel_LoadedPlugin, pluginName);
            }
        }

        /// <summary>
        /// Initializes all of the loaded plugins.
        /// </summary>
        public void InitializePlugins() {
            var loadedPlugins = _pluginTypesToLoad.Select(t => (OrionPlugin)Container.Get(t)).ToList();
            foreach (var plugin in loadedPlugins) {
                plugin.Initialize();

                var pluginType = plugin.GetType();
                var attribute = pluginType.GetCustomAttribute<ServiceAttribute?>();
                var pluginName = attribute?.Name ?? pluginType.Name;
                _plugins[pluginName] = plugin;

                var pluginVersion = pluginType.Assembly.GetName().Version;
                var pluginAuthor = attribute?.Author ?? "Pryaxis";
                _log.Information(Resources.Kernel_InitializedPlugin, pluginName, pluginVersion, pluginAuthor);
            }

            _pluginTypesToLoad.Clear();
        }

        /// <summary>
        /// Unloads the given <paramref name="plugin"/> and returns a value indicating success.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <returns><see langword="true"/> if the plugin was unloaded; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="plugin"/> is <see langword="null"/>.</exception>
        public bool UnloadPlugin(OrionPlugin plugin) {
            if (plugin is null) {
                throw new ArgumentNullException(nameof(plugin));
            }

            var pluginType = plugin.GetType();
            var pluginName = pluginType.GetCustomAttribute<ServiceAttribute?>()?.Name ?? pluginType.Name;
            if (!_plugins.Remove(pluginName)) {
                return false;
            }

            plugin.Dispose();
            Container.Unbind(pluginType);

            _log.Information(Resources.Kernel_UnloadedPlugin, pluginName);
            return true;
        }

        /// <summary>
        /// Registers the given event <paramref name="handler"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="handler">The event handler.</param>
        /// <param name="log">The logger to log to.</param>
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

            GetEventHandlerCollection<TEvent>().RegisterHandler(handler, log);
        }

        /// <summary>
        /// Registers all of the methods marked with <see cref="EventHandlerAttribute"/> as event handlers in the given
        /// <paramref name="handlerObject"/>.
        /// </summary>
        /// <param name="handlerObject">The handler object.</param>
        /// <param name="log">The logger to log to.</param>
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

            var registrations = _handlerRegistrations[handlerObject] = new List<(Type eventType, object handler)>();
            foreach (var method in handlerObject.GetType()
                    .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
                if (method.GetCustomAttribute<EventHandlerAttribute>() is null) {
                    continue;
                }

                var parameters = method.GetParameters();
                if (parameters.Length != 1) {
                    // Not localized because this string is developer-facing.
                    throw new ArgumentException(
                        $"Method `{method.Name}` does not have exactly one argument.", nameof(handlerObject));
                }

                var eventType = parameters[0].ParameterType;
                if (!eventType.IsSubclassOf(typeof(Event))) {
                    // Not localized because this string is developer-facing.
                    throw new ArgumentException(
                        $"Method `{method.Name}` does not have an argument of type `{nameof(Event)}`.",
                        nameof(handlerObject));
                }

                var handlerType = typeof(Action<>).MakeGenericType(eventType);
                var handler = method.CreateDelegate(handlerType, handlerObject);
                _registerHandler.MakeGenericMethod(eventType).Invoke(this, new object[] { handler, log });
                registrations.Add((eventType, handler));
            }
        }

        /// <summary>
        /// Deregisters the given event <paramref name="handler"/>. Returns a value indicating success.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="handler">The event handler.</param>
        /// <param name="log">The logger to log to.</param>
        /// <returns>
        /// <see langword="true"/> if the event handler was successfully deregistered; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handler"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        public bool DeregisterHandler<TEvent>(Action<TEvent> handler, ILogger log) where TEvent : Event {
            if (handler is null) {
                throw new ArgumentNullException(nameof(handler));
            }
            if (log is null) {
                throw new ArgumentNullException(nameof(log));
            }

            return GetEventHandlerCollection<TEvent>().DeregisterHandler(handler, log);
        }

        /// <summary>
        /// Deregisters all of the methods marked with <see cref="EventHandlerAttribute"/> as event handlers in the
        /// given <paramref name="handlerObject"/>. Returns a value indicating success.
        /// </summary>
        /// <param name="handlerObject">The handler object.</param>
        /// <param name="log">The logger to log to.</param>
        /// <returns>
        /// <see langword="true"/> if the event handlers were successfully deregistered; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="handlerObject"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        public bool DeregisterHandlers(object handlerObject, ILogger log) {
            if (handlerObject is null) {
                throw new ArgumentNullException(nameof(handlerObject));
            }
            if (log is null) {
                throw new ArgumentNullException(nameof(log));
            }

            if (!_handlerRegistrations.TryGetValue(handlerObject, out var registrations)) {
                return false;
            }

            _handlerRegistrations.Remove(handlerObject);
            foreach (var (eventType, handler) in registrations) {
                _deregisterHandler.MakeGenericMethod(eventType).Invoke(this, new object[] { handler, log });
            }
            return true;
        }

        /// <summary>
        /// Raises <paramref name="evt"/>.
        /// </summary>
        /// <typeparam name="TEvent">The type of event.</typeparam>
        /// <param name="evt">The event to raise.</param>
        /// <param name="log">The logger to log to.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="evt"/> or <paramref name="log"/> are <see langword="null"/>.
        /// </exception>
        public void Raise<TEvent>(TEvent evt, ILogger log) where TEvent : Event {
            if (evt is null) {
                throw new ArgumentNullException(nameof(evt));
            }
            if (log is null) {
                throw new ArgumentNullException(nameof(log));
            }

            GetEventHandlerCollection<TEvent>().Raise(evt, log);
        }

        // Helper method for creating a generic `Lazy<T>`.
        private Lazy<T> GetLazy<T>() => new Lazy<T>(() => Container.Get<T>());

        // Resolves assemblies by looking through the loaded plugins' assemblies.
        private Assembly AssemblyResolveHandler(object sender, ResolveEventArgs args) =>
            _pluginTypesToLoad
                .Select(t => t.Assembly)
                .Concat(_plugins.Values.Select(p => p.GetType().Assembly))
                .FirstOrDefault(a => a.FullName == args.Name);

        // Helper method for retrieving an `EventHandlerCollection<TEvent>`.
        private EventHandlerCollection<TEvent> GetEventHandlerCollection<TEvent>() where TEvent : Event {
            var type = typeof(TEvent);
            if (!_eventHandlerCollections.TryGetValue(type, out var collection)) {
                collection = new EventHandlerCollection<TEvent>();
                _eventHandlerCollections[type] = collection;
            }

            return (EventHandlerCollection<TEvent>)collection;
        }

        private void PreInitializeHandler() {
            var evt = new ServerInitializeEvent();
            Raise(evt, _log);
        }

        private void PostInitializeHandler() {
            var evt = new ServerInitializedEvent();
            Raise(evt, _log);
        }

        private void StartedHandler() {
            var evt = new ServerStartEvent();
            Raise(evt, _log);
        }

        private OTAPI.HookResult ProcessHandler(string _, string input) {
            var evt = new ServerCommandEvent(input);
            Raise(evt, _log);
            return evt.IsCanceled() ? OTAPI.HookResult.Cancel : OTAPI.HookResult.Continue;
        }
    }
}
