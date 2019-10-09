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
using Orion.World;
using OTAPI;

namespace Orion {
    /// <summary>
    /// Represents Orion's dependency injection container. Provides methods to manipulate <see cref="OrionPlugin"/>
    /// instances.
    /// </summary>
    public sealed class OrionKernel : StandardKernel {
        private readonly ISet<Assembly> _pluginAssemblies = new HashSet<Assembly>();
        private readonly ISet<Type> _pluginTypesToLoad = new HashSet<Type>();
        private readonly ISet<OrionPlugin> _plugins = new HashSet<OrionPlugin>();

        /// <summary>
        /// Gets the loaded plugins.
        /// </summary>
        public IEnumerable<OrionPlugin> LoadedPlugins => new HashSet<OrionPlugin>(_plugins);

        /// <summary>
        /// Gets or sets the events that occur when the server initializes.
        /// </summary>
        public EventHandlerCollection<ServerInitializeEventArgs>? ServerInitialize { get; set; }

        /// <summary>
        /// Gets or sets the events that occur when the server starts.
        /// </summary>
        public EventHandlerCollection<ServerStartEventArgs>? ServerStart { get; set; }

        /// <summary>
        /// Gets or sets the events that occur when the server updates.
        /// </summary>
        public EventHandlerCollection<ServerUpdateEventArgs>? ServerUpdate { get; set; }

        /// <summary>
        /// Gets or sets the events that occur when the server executes a command. This event can be canceled.
        /// </summary>
        public EventHandlerCollection<ServerCommandEventArgs>? ServerCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionKernel"/> class.
        /// </summary>
        public OrionKernel() {
            Bind<OrionKernel>().ToConstant(this).InSingletonScope();
            Bind<IItemService>().To<OrionItemService>().InSingletonScope();
            Bind<INpcService>().To<OrionNpcService>().InSingletonScope();
            Bind<IPlayerService>().To<OrionPlayerService>().InSingletonScope();
            Bind<IProjectileService>().To<OrionProjectileService>().InSingletonScope();
            Bind<IWorldService>().To<OrionWorldService>().InSingletonScope();

            // Create bindings for Lazy<T> so that we can have lazily loaded services, allowing plugins to override
            // service bindings if necessary.
            var getLazy = GetType().GetMethod(nameof(GetLazy), BindingFlags.NonPublic | BindingFlags.Instance);
            Bind(typeof(Lazy<>)).ToMethod(ctx => getLazy
                .MakeGenericMethod(ctx.GenericArguments?[0])
                .Invoke(this, Array.Empty<object>()));

            // Because we're using Assembly.Load, we'll need to have an AssemblyResolve handler to deal with assembly
            // resolution issues.
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolveHandler;

            Hooks.Game.PreInitialize += PreInitializeHandler;
            Hooks.Game.Started += StartedHandler;
            Hooks.Game.PreUpdate += PreUpdateHandler;
            Hooks.Command.Process += ProcessHandler;
        }

        /// <inheritdoc/>
        public override void Dispose(bool disposing) {
            base.Dispose(disposing);

            AppDomain.CurrentDomain.AssemblyResolve -= AssemblyResolveHandler;

            Hooks.Game.PreInitialize -= PreInitializeHandler;
            Hooks.Game.Started -= StartedHandler;
            Hooks.Game.PreUpdate -= PreUpdateHandler;
            Hooks.Command.Process -= ProcessHandler;
        }

        /// <summary>
        /// Queues plugins to be loaded from <paramref name="assemblyPath"/>.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="assemblyPath"/> is <see langword="null"/>.
        /// </exception>
        public void QueuePluginsFromPath(string assemblyPath) {
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
            }
        }

        /// <summary>
        /// Finishes loading plugins, running the given <paramref name="action"/> for each plugin loaded.
        /// </summary>
        /// <param name="action">The action to run.</param>
        public void FinishLoadingPlugins(Action<OrionPlugin>? action = null) {
            foreach (var pluginType in _pluginTypesToLoad) {
                var plugin = (OrionPlugin)this.Get(pluginType);
                plugin.Initialize();
                _plugins.Add(plugin);
                action?.Invoke(plugin);
            }

            _pluginTypesToLoad.Clear();
        }

        /// <summary>
        /// Unloads <paramref name="plugin"/> and returns a value indicating success.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <exception cref="ArgumentNullException"><paramref name="plugin"/> is <see langword="null"/>.</exception>
        /// <returns><see langword="true"/> if the plugin was unloaded; otherwise, <see langword="false"/>.</returns>
        public bool UnloadPlugin(OrionPlugin plugin) {
            if (plugin is null) {
                throw new ArgumentNullException(nameof(plugin));
            }

            if (!_plugins.Contains(plugin)) {
                return false;
            }

            var pluginType = plugin.GetType();
            _pluginAssemblies.Remove(pluginType.Assembly);
            _plugins.Remove(plugin);

            plugin.Dispose();
            Unbind(pluginType);
            return true;
        }

        private Lazy<T> GetLazy<T>() => new Lazy<T>(() => this.Get<T>());

        private Assembly AssemblyResolveHandler(object sender, ResolveEventArgs args) =>
            _pluginAssemblies.FirstOrDefault(a => a.FullName == args.Name);

        private void PreInitializeHandler() {
            var args = new ServerInitializeEventArgs();
            ServerInitialize?.Invoke(this, args);
        }

        private void StartedHandler() {
            var args = new ServerStartEventArgs();
            ServerStart?.Invoke(this, args);
        }

        private void PreUpdateHandler(ref GameTime _) {
            var args = new ServerUpdateEventArgs();
            ServerUpdate?.Invoke(this, args);
        }

        private HookResult ProcessHandler(string _, string input) {
            var args = new ServerCommandEventArgs(input);
            ServerCommand?.Invoke(this, args);
            return args.IsCanceled() ? HookResult.Cancel : HookResult.Continue;
        }
    }
}
