using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Ninject;
using Ninject.Extensions.NamedScope;
using Orion.Items;
using Orion.Networking;
using Orion.Npcs;
using Orion.Players;
using Orion.Projectiles;
using Orion.World;
using Orion.World.TileEntities;

namespace Orion {
    /// <summary>
    /// Manages Orion's dependency injection.
    /// </summary>
    public sealed class OrionKernel : StandardKernel {
        private static ISet<Type> InterfaceTypesToIgnore => new HashSet<Type> {
            typeof(IDisposable),
            typeof(IReadOnlyList<>),
            typeof(IService)
        };

        private readonly ISet<Assembly> _pluginAssemblies = new HashSet<Assembly>();
        private readonly ISet<Type> _pluginTypesToLoad = new HashSet<Type>();
        private readonly ISet<OrionPlugin> _plugins = new HashSet<OrionPlugin>();

        internal OrionKernel() {
            Bind<OrionKernel>().ToConstant(this).InSingletonScope();
            Bind<IItemService>().To<OrionItemService>().InSingletonScope();
            Bind<INetworkService>().To<OrionNetworkService>().InSingletonScope();
            Bind<INpcService>().To<OrionNpcService>().InSingletonScope();
            Bind<IPlayerService>().To<OrionPlayerService>().InSingletonScope();
            Bind<IProjectileService>().To<OrionProjectileService>().InSingletonScope();
            Bind<IChestService>().To<OrionChestService>().InSingletonScope();
            Bind<ISignService>().To<OrionSignService>().InSingletonScope();
            Bind<IWorldService>().To<OrionWorldService>().InSingletonScope();
            
            // Because we're using Assembly.Load, we'll need to have an AssemblyResolve handler to deal with any issues
            // that may pop up.
            AppDomain.CurrentDomain.AssemblyResolve +=
                (sender, args) => _pluginAssemblies.First(a => a.FullName == args.Name);
        }

        /// <summary>
        /// Gets the loaded plugins.
        /// </summary>
        /// <returns>The loaded plugins.</returns>
        public IEnumerable<OrionPlugin> GetLoadedPlugins() => new HashSet<OrionPlugin>(_plugins);

        /// <summary>
        /// Queues plugins to be loaded from the given assembly path.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <exception cref="ArgumentNullException"><paramref name="assemblyPath"/> is <c>null</c>.</exception>
        public void QueuePluginsFromPath(string assemblyPath) {
            if (assemblyPath == null) {
                throw new ArgumentNullException(nameof(assemblyPath));
            }

            // Load the assembly from the path. We're using Assembly.Load with the bytes of the file so that we don't
            // hold a lock on the path, allowing the user to delete or upgrade the path.
            var assembly = Assembly.Load(File.ReadAllBytes(assemblyPath));
            _pluginAssemblies.Add(assembly);

            // Because GetExportedTypes returns generic types that are 'bound', we may potentially need to get unbound
            // versions of the types.
            Type GetGenericTypeMaybe(Type type) => type.IsGenericType ? type.GetGenericTypeDefinition() : type;

            foreach (var serviceType in assembly.GetExportedTypes()
                                                .Where(s => s.IsSubclassOf(typeof(OrionService)))
                                                .Select(GetGenericTypeMaybe)) {
                if (serviceType.IsSubclassOf(typeof(OrionPlugin))) {
                    // Bind all plugin types to themselves, allowing plugins to properly depend on other plugins
                    // without reliance on static state.
                    _pluginTypesToLoad.Add(serviceType);
                    Bind(serviceType).ToSelf().InSingletonScope();
                    continue;
                }

                var isInstanced = serviceType.GetCustomAttribute<InstancedServiceAttribute>() != null;

                // Bind all service interfaces to the implementation. We use a contextual binding here which will
                // override any default bindings that we've set in the constructor of OrionKernel.
                foreach (var interfaceType in serviceType.GetInterfaces()
                                                         .Where(i => !InterfaceTypesToIgnore.Contains(i))
                                                         .Select(GetGenericTypeMaybe)) {
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
        /// Finishes loading plugins, running the given action for each plugin loaded.
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
        /// Unloads the given plugin.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <exception cref="ArgumentNullException"><paramref name="plugin"/> is <c>null</c>.</exception>
        public bool UnloadPlugin(OrionPlugin plugin) {
            if (plugin == null) {
                throw new ArgumentNullException(nameof(plugin));
            }

            if (_plugins.Contains(plugin)) {
                var pluginType = plugin.GetType();
                _pluginAssemblies.Remove(pluginType.Assembly);
                _plugins.Remove(plugin);
                
                plugin.Dispose();
                Unbind(pluginType);
                return true;
            } else {
                return false;
            }
        }
    }
}
