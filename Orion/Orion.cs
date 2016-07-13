using Ninject;
using Orion.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Terraria;

namespace Orion
{
    public class Orion : IDisposable
    {
        protected string[] CommandArguments { get; set; }

        /// <summary>
        /// A list of directories that must exist under orion's directory
        /// </summary>
        protected string[] standardDirectories = new[]
        {
            "plugins"
        };

        /// <summary>
        /// Contains a reference to the injection container, which contains references
        /// to plugins and services.
        /// </summary>
        protected IKernel injectionContainer;

        public Orion(params string[] args)
        {
            this.CommandArguments = args;

            CreateDirectories();

            this.injectionContainer = new StandardKernel(
                new Framework.Injection.OrionInjectModule()
                );
        }

        /// <summary>
        /// Enumerates through all directories in Orion's standard directory list
        /// and creates them if they don't exist.
        /// </summary>
        public void CreateDirectories()
        {
            foreach (string dir in standardDirectories)
            {
                if (Directory.Exists(dir))
                {
                    continue;
                }

                Directory.CreateDirectory(dir);
            }
        }

        public void StartServer()
        {
            foreach (IService service in injectionContainer.GetAll<IService>())
            {
                Console.WriteLine($"  * Loading {service.Name} by {service.Author}");
            }

            WindowsLaunch.Main(CommandArguments);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    injectionContainer.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
