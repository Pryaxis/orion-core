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
using System.Diagnostics;
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

        /// <summary>
        /// Gets a read-only mapping from plugin names to <see cref="OrionPlugin"/> instances.
        /// </summary>
        /// <value>A read-only mapping from plugin names to plugins.</value>
        public IReadOnlyDictionary<string, OrionPlugin> Plugins => _plugins;

        /// <summary>
        /// Gets the events that occur when the server initializes.
        /// </summary>
        /// <value>The events that occur when the server initializes.</value>
        /// <remarks>This event is invoked once the server reaches the world loading stage.</remarks>
        public EventHandlerCollection<ServerInitializeEventArgs> ServerInitialize { get; }

        /// <summary>
        /// Gets the events that occur when the server starts.
        /// </summary>
        /// <value>The events that occur when the server starts.</value>
        /// <remarks>This event is invoked once the server starts accepting connections.</remarks>
        public EventHandlerCollection<ServerStartEventArgs> ServerStart { get; }

        /// <summary>
        /// Gets the events that occur when the server updates.
        /// </summary>
        /// <value>The events that occur when the server updates.</value>
        public EventHandlerCollection<ServerUpdateEventArgs> ServerUpdate { get; }

        /// <summary>
        /// Gets the events that occur when the server executes a command. This event can be canceled.
        /// </summary>
        /// <value>The events that occur when the server executes a command.</value>
        public EventHandlerCollection<ServerCommandEventArgs> ServerCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionKernel"/> class with the specified log.
        /// </summary>
        /// <param name="log">The log. This log will be used to create all injected logs.</param>
        /// <exception cref="ArgumentNullException"><paramref name="log"/> is <see langword="null"/>.</exception>
        public OrionKernel(ILogger log) {
            if (log is null) {
                throw new ArgumentNullException(nameof(log));
            }

            Bind<OrionKernel>().ToConstant(this).InSingletonScope();
            Bind<IItemService>().To<OrionItemService>().InSingletonScope();
            Bind<INpcService>().To<OrionNpcService>().InSingletonScope();
            Bind<IPlayerService>().To<OrionPlayerService>().InSingletonScope();
            Bind<IProjectileService>().To<OrionProjectileService>().InSingletonScope();
            Bind<ITileEntityService>().To<OrionTileEntityService>().InSingletonScope();
            Bind<IWorldService>().To<OrionWorldService>().InSingletonScope();

            // Create an ILogger binding for service-specific logs.
            Bind<ILogger>().ToMethod(ctx => {
                // ctx.Request.Target can be null if the ILogger is requested directly, so we need to be safe about it.
                var type = ctx.Request.Target?.Member.ReflectedType;
                var serviceName = type?.GetCustomAttribute<ServiceAttribute>()?.Name ?? type?.Name ?? "orion-kernel";
                return log.ForContext("ServiceName", serviceName);
            }).InTransientScope();

            _log = this.Get<ILogger>();

            ServerInitialize = new EventHandlerCollection<ServerInitializeEventArgs>();
            ServerStart = new EventHandlerCollection<ServerStartEventArgs>();
            ServerUpdate = new EventHandlerCollection<ServerUpdateEventArgs>();
            ServerCommand = new EventHandlerCollection<ServerCommandEventArgs>();

            // Create bindings for Lazy<T> so that we can have lazily loaded services. This allows plugins to override
            // the above service bindings if necessary.
            var getLazy = GetType().GetMethod(nameof(GetLazy), BindingFlags.NonPublic | BindingFlags.Instance);
            Bind(typeof(Lazy<>))
                .ToMethod(ctx => getLazy
                    .MakeGenericMethod(ctx.GenericArguments?[0])
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
                // Bind all plugin types to themselves, allowing plugins to properly depend on other plugins without
                // reliance on static state.
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

        private Lazy<T> GetLazy<T>() => new Lazy<T>(() => this.Get<T>());

        private Assembly AssemblyResolveHandler(object sender, ResolveEventArgs args) =>
            _pluginAssemblies.FirstOrDefault(a => a.FullName == args.Name);

        // =============================================================================================================
        // Handling ServerInitialize
        // =============================================================================================================

        private void PreInitializeHandler() {
            var args = new ServerInitializeEventArgs();

            LogServerInitialize();
            ServerInitialize.Invoke(this, args);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogServerInitialize() {
            // Not localized because this string is developer-facing.
            _log.Debug("Invoking {Event}", ServerInitialize);
        }

        // =============================================================================================================
        // Handling ServerStart
        // =============================================================================================================

        private void StartedHandler() {
            var args = new ServerStartEventArgs();

            LogServerStart();
            ServerStart.Invoke(this, args);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogServerStart() {
            // Not localized because this string is developer-facing.
            _log.Debug("Invoking {Event}", ServerStart);
        }

        // =============================================================================================================
        // Handling ServerUpdate
        // =============================================================================================================

        private void PreUpdateHandler(ref GameTime _) {
            var args = new ServerUpdateEventArgs();

            LogServerUpdate();
            ServerUpdate.Invoke(this, args);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogServerUpdate() {
            // Not localized because this string is developer-facing.
            _log.Verbose("Invoking {Event}", ServerUpdate);
        }

        // =============================================================================================================
        // Handling ServerCommand
        // =============================================================================================================

        private HookResult ProcessHandler(string _, string input) {
            var args = new ServerCommandEventArgs(input);

            LogServerCommand_Before(input);
            ServerCommand.Invoke(this, args);
            LogServerCommand_After(args);

            return args.IsCanceled() ? HookResult.Cancel : HookResult.Continue;
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogServerCommand_Before(string input) {
            // Not localized because this string is developer-facing.
            _log.Debug("Invoking {Event} with [{Input}]", ServerCommand, input);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogServerCommand_After(ServerCommandEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                _log.Debug("Canceled {Event} for {Reason}", ServerCommand, args.CancellationReason);
            }
        }
    }
}
