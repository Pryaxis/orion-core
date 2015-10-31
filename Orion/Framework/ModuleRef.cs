using OTA.DebugFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Orion.Framework
{
    /// <summary>
    /// Houses a reference-counted instance of an Orion module, with the ability to return both
    /// strong and weak references to the loaded module.
    /// </summary>
    public class ModuleRef : IDisposable
    {
        protected Orion coreRef;
        protected OrionModuleBase moduleInstance;
        protected int moduleDepCount;
        protected Type moduleType;

        /// <summary>
        /// Gets the type of the module referenced by this ModuleRef instance.
        /// </summary>
        public Type ModuleType => moduleType;

        /// <summary>
        /// Gets a weak reference to the loaded module.
        /// </summary>
        public WeakReference<TModule> GetWeakReference<TModule>() where TModule : OrionModuleBase => new WeakReference<TModule>(moduleInstance as TModule);

        /// <summary>
        /// Gets a strong reference to the loaded module.  Do not store this variable anywhere,
        /// use <see cref="WeakReference"/> instead.
        /// </summary>
        /// <returns>
        /// A strong reference to the module inside this ModuleRef, casted as <typeparamref name="TModule"/>, or
        /// null if the instance does not exist or could not be reference casted to <typeparamref name="TModule"/>
        /// </returns>
        public TModule GetStrongReference<TModule>() where TModule : OrionModuleBase => moduleInstance as TModule;

        /// <summary>
        /// Creates a new instance of the Orion module this ModuleRef instance points to.
        /// </summary>
        /// <returns>
        /// A reference to the new OrionModule instance.
        /// </returns>
        internal OrionModuleBase CreateInstance()
        {
            Assert.Expression(() => moduleInstance != null);
            moduleInstance = Activator.CreateInstance(moduleType, coreRef) as OrionModuleBase;

            return GetStrongReference<OrionModuleBase>();
        }

        /// <summary>
        /// Creates a new <typeparamref name="TModule"/> instance this ModuleRef instance points to
        /// </summary>
        /// <typeparam name="TModule">
        /// TModule is the type of module the instance should be cast to
        /// </typeparam>
        /// <returns>A <typeparamref name="TModule"/> instance</returns>
        internal TModule CreateInstance<TModule>()
            where TModule : OrionModuleBase
        {
            return CreateInstance() as TModule;
        }

        /// <summary>
        /// Called when a [DependsOn] depends on a module, increments the module's dependency
        /// reference count so that the module with the most references gets loaded first.
        /// </summary>
        internal void IncrementDependencyCount()
        {
            Interlocked.Increment(ref moduleDepCount);
        }

        /// <summary>
        /// Gets the number of times the module has been referenced by [DependsOn] attributes set
        /// on other modules to be loaded.
        /// </summary>
        internal int DependencyCount => moduleDepCount;

        /// <summary>
        /// Returns the list of module dependencies via the [DependsOn] attribute marked on the
        /// target Orion module
        /// </summary>
        public DependsOnAttribute DependsOnAttribute
        {
            get
            {
                Assert.Expression(() => moduleType.IsAssignableFrom(typeof(OrionModuleBase)));
                return Attribute.GetCustomAttribute(moduleType, typeof(DependsOnAttribute)) as DependsOnAttribute;
            }
        }

        /// <summary>
        /// Returns the [OrionModule] metadata featured for this Orion module type, used
        /// to gain access to author and other metdata on the Orion module.
        /// </summary>
        public OrionModuleAttribute ModuleAttribute
        {
            get
            {
                Assert.Expression(() => moduleType.IsAssignableFrom(typeof(OrionModuleBase)));
                return Attribute.GetCustomAttribute(moduleType, typeof(OrionModuleAttribute)) as OrionModuleAttribute;
            }
        }

        /// <summary>
        /// Intitalizes a new module reference container for the provided module instance.
        /// </summary>
        /// <param name="module">
        /// A reference to the module in which to make the module reference container for
        /// </param>
        public ModuleRef(Orion core, Type moduleType)
        {
            this.coreRef = core;
            this.moduleType = moduleType;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    moduleInstance.Dispose();
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ModuleRef() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
