using Orion.Extensions;
using Orion.Framework;
using OTA.DebugFramework;
using OTA.Logging;
using OTA.Plugin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Orion
{
    /// <summary>
    /// The core center piece of Orion. All Orion modules will have a reference
    /// to this Orion core.
    /// </summary>
    public partial class Orion : IDisposable
    {
        private bool disposedValue = false; // To detect redundant calls
        private readonly OTAPIPlugin plugin;

        public const string kOrionBasePath = "Orion";

        /// <summary>
        /// Gets a reference to the OTAPI plugin container in which this Orion core
        /// runs inside of
        /// </summary>
        /// <remarks>
        /// Marked as internal, Orion modules should be using hooks from the IHookProvider
        /// interface Orion provides itself
        /// </remarks>
        internal OTAPIPlugin Plugin => plugin;

        /// <summary>
        /// Contains references to all the Orion modules loaded inside Orion
        /// </summary>
        internal readonly List<ModuleRef> moduleContainer;

        /// <summary>
        /// Returns the base path for all Orion-based disk information
        /// </summary>
        public string OrionBasePath { get; set; } = kOrionBasePath;

        /// <summary>
        /// Gets the Orion module directory path 
        /// </summary>
        public string OrionModulePath => Path.Combine(OrionBasePath, "Modules");

        /// <summary>
        /// Gets the Orion databse directory path
        /// </summary>
        public string OrionDatabasePath => Path.Combine(OrionBasePath, "Database");

        /// <summary>
        /// Gets the Orion configuration directory path
        /// </summary>
        public string OrionConfigurationPath => Path.Combine(OrionBasePath, "Configuration");

        /// <summary>
        /// Gets the version of Orion
        /// </summary>
        public Version Version => Assembly.GetCallingAssembly().GetName().Version;

        /// <summary>
        /// Internal synch root for Orion to lock on when access to a member must be thread-safe.
        /// </summary>
        protected readonly object syncRoot = new object();

        protected int initPercent = 0;

        public Orion(OTAPIPlugin plugin)
        {
            this.moduleContainer = new List<ModuleRef>();
            this.plugin = plugin;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }


        public void Initialize()
        {
            PrintConsoleHeader();
            CheckDirectories();

            /*
             * Loads all module types as ModuleRef instances inside the container.
             */
            LoadModules();
            
            /*
             * Updates the dependency graph, so that modules that have the most
             * dependencies via the [DependsOn] attribute load first.
             */
            UpdateDependencyGraph();

            /*
             * Runs all modules inside the module container.
             */
            CreateModuleInstances();
        }

        /// <summary>
        /// Prints some mad console shinies to the screen.
        /// </summary>
        protected void PrintConsoleHeader()
        {
            Console.Clear();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("O");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("rion API");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\tv{Version.Major}.{Version.Minor}\tProudly by Nyx Studios");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Orion is loading...");

            PrintConsoleProgressBar(4, ++initPercent, "Initializing");

            Console.WriteLine();
            Console.WriteLine();
        }


        /// <summary>
        /// Prints a console progress bar at position <paramref name="y"/> with the
        /// specfied <paramref name="percent"/>.  Optionally contains up to a 15
        /// character <paramref name="message"/>.
        /// </summary>
        /// <param name="y">
        /// The Y offset position in the console window in which to print 
        /// the progress bar.
        /// </param>
        /// <param name="percent">
        /// A whole percentage of the progress bar (out of 100%) to fill
        /// </param>
        /// <param name="message">
        /// An optional message to display with the progress bar.
        /// </param>
        protected void PrintConsoleProgressBar(int y, int percent, string message = "")
        {
            ConsoleColor background = Console.BackgroundColor;
            int barSize = 0;

            int originalX = Console.CursorLeft;
            int originalY = Console.CursorTop;

            Console.CursorTop = y;
            Console.CursorLeft = 0;

            for (int i = 0; i < 15; i++)
            {
                char c;
                if (i >= message.Length)
                {
                    c = ' ';
                }
                else
                {
                    c = message[i];
                }

                Console.Write(c);
            }

            Console.Write(" [");

            barSize = Console.WindowWidth - Console.CursorLeft - 3;
            for (int i = Console.CursorLeft, zeroIndex = 0; i < barSize; i++, zeroIndex++)
            {
                decimal size = ((decimal)zeroIndex / barSize) * 100;

                if (percent <= size)
                {
                    Console.BackgroundColor = background;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                }

                Console.Write(" ");
            }

            Console.BackgroundColor = background;
            Console.Write("]");
            Console.SetCursorPosition(originalX, originalY);
        }


        /// <summary>
        /// Checks for, and creates Orion's directory structure if it doesn't exist
        /// </summary>
        protected void CheckDirectories()
        {
            if (Directory.Exists(OrionBasePath) == false)
            {
                Directory.CreateDirectory(OrionBasePath);
                Directory.CreateDirectory(OrionModulePath);
                Directory.CreateDirectory(OrionConfigurationPath);
                Directory.CreateDirectory(OrionDatabasePath);
            }
            else
            {
                if (Directory.Exists(OrionModulePath) == false)
                {
                    Directory.CreateDirectory(OrionModulePath);
                }

                if (Directory.Exists(OrionConfigurationPath) == false)
                {
                    Directory.CreateDirectory(OrionConfigurationPath);
                }

                if (Directory.Exists(OrionDatabasePath) == false)
                {
                    Directory.CreateDirectory(OrionDatabasePath);
                }
            }

            PrintConsoleProgressBar(4, ++initPercent, "Checking");
        }

        /// <summary>
        /// Returns the first orion module matching the type parameter <typeparamref name="TModule"/>
        /// </summary>
        /// <typeparam name="TModule">
        /// TModule is the type of an Orion module directly inheriting from OrionModuleBase.
        /// </typeparam>
        /// <returns>
        /// A <typeparamref name="TModule"/> instance if one was found in the module container
        /// matching the specified type, or null if none could be found.
        /// </returns>
        public TModule Get<TModule>()
            where TModule : OrionModuleBase
        {
            TModule module = null;

            lock (syncRoot)
            {
                module = moduleContainer
                    .FirstOrDefault(i => i.ModuleType == typeof(TModule))
                    .GetStrongReference<TModule>();
            }

            return module;
        }

        #region Orion Module Loader

        /// <summary>
        /// Runs all Orion modules in module-dependent order.
        /// </summary>
        public void CreateModuleInstances()
        {
            List<OrionModuleBase> failedModules = new List<OrionModuleBase>();
            OrionModuleBase moduleInstance = null;

            lock (syncRoot)
            {
                int count = 0;
                foreach (ModuleRef module in moduleContainer.OrderByDescending(i => i.DependencyCount))
                {
                    try
                    {
                        moduleInstance = module.CreateInstance();
                        moduleInstance.Initialize();
                    }
                    catch (Exception ex) when (!(ex is AssertionException))
                    {
                        /*
                         * Module init exceptions should not interfere with other module inits.
                         */
                        failedModules.Add(moduleInstance);
                        continue;
                    }
                    finally
                    {
                        PrintConsoleProgressBar(4, (int)((decimal)++count / moduleContainer.Count) * 100, module.ModuleAttribute.ModuleName.GenerateSlug());
                    }
                }

                if (failedModules.Count > 0)
                {
                    ProgramLog.Error.Log($"orion modules:  These following modules failed to initialize and were disabled.");

                    foreach (OrionModuleBase failedModule in failedModules)
                    {
                        ProgramLog.Error.Log($" * {failedModule.ModuleName} by {failedModule.Author}");
                        failedModule.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Loads all modules into the module container.
        /// </summary>
        public void LoadModules()
        {
            PrintConsoleProgressBar(4, ++initPercent, "loadmod");

            /*
             * All OrionModule types inside Orion.dll itself are considered internal.
             */
            IEnumerable<Type> internalModuleList = GetOrionModulesFromAssembly(typeof(Orion).Assembly);
            PrintConsoleProgressBar(4, ++initPercent, "loadmod-int");

            /*
             * Note:  Internal modules will load first before all other modules, completely
             * disregarding the global order.  So the internal module list is *unioned* with
             * the external module list so that internal ones always get instantiated first.
             */
            IEnumerable<Type> externalModuleList = ExternalModules();
            PrintConsoleProgressBar(4, ++initPercent, "loadmod-ext");

            foreach (Type module in internalModuleList.Union(externalModuleList))
            {
                OrionModuleAttribute attr = Attribute.GetCustomAttribute(module, typeof(OrionModuleAttribute)) as OrionModuleAttribute;

                if (attr == null)
                {
                    continue;
                }

                try
                {
                    LoadModule(module);
                }
                catch
                {
                    ProgramLog.Log($"orion modules: Could not load module {attr.ModuleName} because of an error", ConsoleColor.Yellow);
                    continue;
                }
            }
        }

        /// <summary>
        /// Scans an entire Assembly definition for Orion modules that are not disabled,
        /// and returns the types of those modules in OrionModule-qualified order.
        /// </summary>
        /// <param name="asm">
        /// A reference to the assembly to scan for Orion modules in
        /// </param>
        /// <returns>
        /// An array of types of valid orion modules.
        /// </returns>
        public IEnumerable<Type> GetOrionModulesFromAssembly(Assembly asm)
        {
            return from i in asm.GetTypes()
                   let orionModuleAttr = Attribute.GetCustomAttribute(i, typeof(OrionModuleAttribute)) as OrionModuleAttribute
                   where orionModuleAttr != null
                       && orionModuleAttr.Enabled == true
                       && i.BaseType == typeof(OrionModuleBase)
                   select i;
        }

        /// <summary>
        /// Enumerates all DLL assemblies in the specified directory, and returns a
        /// list of all enabled OrionModules in those assemblies.
        /// </summary>
        /// <param name="pluginsDirectory">
        /// (Optional) A reference to the plugins directory to search in.  Defaults to
        /// OTA's plugins directory.
        /// </param>
        public IEnumerable<Type> ExternalModules()
        {
            Assembly asm;
            List<Type> moduleList = new List<Type>();

            foreach (string pluginFile in Directory.EnumerateFiles(OrionModulePath, "*.dll"))
            {
                ProgramLog.Debug.Log($"orion modules: trying assembly {pluginFile}");
                try
                {
                    asm = Assembly.LoadFrom(pluginFile);
                }
                catch (Exception ex) when (ex is BadImageFormatException || ex is FileLoadException)
                {
                    //TODO: Print warning about assembly being ignored because it was invalid
                    continue;
                }

                /*
                 * Important:  Do not do orderby selectors here, as they are re-ordered later
                 * in the process.
                 */
                moduleList.AddRange(GetOrionModulesFromAssembly(asm));
            }

            return moduleList;
        }

        /// <summary>
        /// Loads a module type and inserts it into the module container.
        /// </summary>
        /// <param name="moduleType">
        /// A reference to the runtime type of the module to load
        /// </param>
        public void LoadModule(Type moduleType)
        {
            lock (syncRoot)
            {
                moduleContainer.Add(new ModuleRef(this, moduleType));
            }
        }

        /// <summary>
        /// Loops through each module dependency listed in the [DependsOn] attribute
        /// on each module in the module container, and increments the dependency
        /// count on the target module.  Must be called after the module container is
        /// fully loaded with all the module references.
        /// </summary>
        internal void UpdateDependencyGraph()
        {
            lock (syncRoot)
            {
                foreach (ModuleRef mod in moduleContainer)
                {
                    if (mod.DependsOnAttribute == null)
                    {
                        continue;
                    }

                    foreach (Type dependency in mod.DependsOnAttribute.ModuleDependencies)
                    {
                        ModuleRef dependencyRef;

                        if (dependency.IsSubclassOf(typeof(OrionModuleBase)) == false)
                        {
                            continue;
                        }

                        dependencyRef = GetModuleRef(dependency);
                        Assert.Expression(() => dependencyRef == null);
                        
                        dependencyRef.IncrementDependencyCount();
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves the ModuleRef instance for the specified Orion module type from 
        /// Orion's module container.
        /// </summary>
        /// <param name="moduleType">A type of any Orion module that inherits from <see cref="OrionModuleBase"/></param>
        /// <returns>
        /// A ModuleRef containing the orion module type if one was found,
        /// or null if one does not exist.
        /// </returns>
        public ModuleRef GetModuleRef(Type moduleType)
        {
            Assert.Expression(() => moduleType == null);
            Assert.Expression(() => moduleType.IsSubclassOf(typeof(OrionModuleBase)) == false);
            Assert.Expression(() => moduleContainer.FirstOrDefault(i => i.ModuleType == moduleType) == null);

            lock (syncRoot)
            {
                return moduleContainer.FirstOrDefault(i => i.ModuleType == moduleType);
            }
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string asmName = args.Name.Split(',')[0];

            /*
             * WORKAROUND
             * 
             * Do not return new copies of Orion itself.  It is not clear why AssemblyResolve
             * gets called on Orion, when Orion is already loaded, and returning new copies of
             * Orion (even though it is the same assembly) will break type equality procedures.
             */
            if (asmName == "Orion")
            {
                return typeof(Orion).Assembly;
            }

            var paths = new[] {
                Path.Combine(OrionModulePath, asmName + ".dll"),
                Path.Combine(OrionBasePath, asmName + ".dll"),
            };

            foreach (string path in paths)
            {
                if (File.Exists(path) == false)
                {
                    continue;
                }

                try
                {
                    return Assembly.LoadFile(path);
                }
                catch (FileNotFoundException)
                {
                    ProgramLog.Error.Log($"orion modules: {path} skipped: file not found");
                }
                catch
                {
                    ProgramLog.Error.Log($"orion modules: {path} skipped: load error");
                }
            }

            ProgramLog.Error.Log($"orion modules: no candidate for assembly {asmName}");
            return null;
        }
        #endregion

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        ~Orion()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
