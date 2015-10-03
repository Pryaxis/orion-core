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

        public Orion(OTAPIPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void Initialize()
        {
            plugin.Hook(HookPoints.StartCommandProcessing, HookOrder.FIRST, HookPoint_StartCommandProcessing);
        }

        private void HookPoint_StartCommandProcessing(ref HookContext context, ref HookArgs.StartCommandProcessing argument)
        {
            context.SetResult(HookResult.IGNORE);
        }

        protected void LoadModules()
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
