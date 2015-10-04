using Orion.Framework;
using OTA.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion
{
    /// <summary>
    /// The core center piece of Orion. All Orion modules will have a reference
    /// to this Orion core.
    /// </summary>
    public class Orion : IDisposable
    {
        private bool disposedValue = false; // To detect redundant calls
        private readonly OTAPIPlugin plugin;

        /// <summary>
        /// Gets a reference to the OTAPI plugin container in which this Orion core
        /// runs inside of
        /// </summary>
        /// <remarks>
        /// Marked as internal, Orion modules should be using hooks from the IHookProvider
        /// interface Orion provides itself
        /// </remarks>
        internal OTAPIPlugin Plugin => plugin;

        public Orion(OTAPIPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Initialize()
        {
        }

        protected void LoadModules()
        {
            List<Type> internalModules = new List<Type>();
            List<Type> externalModules = new List<Type>();

            LoadInternalModules(internalModules);
            LoadExternalModules(externalModules);
        }

        protected void LoadInternalModules(List<Type> modules)
        {

        }

        protected void LoadExternalModules(List<Type> modules)
        {

        }

        protected void LoadModule(OrionModuleBase module)
        {
        }
       
        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    plugin.Unhook(HookPoints.StartCommandProcessing);

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
