using System;

namespace Orion.Framework
{
    /// <summary>
    /// Abstract class that all Orion modules must derive from to implement an Orion module
    /// </summary>
	public abstract class OrionModuleBase : IDisposable
    {
        private bool disposed = false;
        private OrionModuleAttribute moduleAttr;
        private Orion coreRef; // Derivatives should access Orion through the Core property, not this field

        /// <summary>
        /// Gets a reference to the Orion core for interacting with other Orion modules
        /// </summary>
        protected Orion Core => coreRef;

        /// <summary>
        /// Gets the author of the module
        /// </summary>
        public string Author => moduleAttr.Author;

        /// <summary>
        /// Gets the name of the module
        /// </summary>
        public string ModuleName => moduleAttr.ModuleName;

        /// <summary>
        /// Gets the version of the module
        /// </summary>
        public Version ModuleVersion => moduleAttr.ModuleVersion;

        /// <summary>
        /// Gets a value determining whether the module is loaded by OTAPI
        /// </summary>
        public bool Enabled => moduleAttr.Enabled;

        /// <summary>
        /// Initializes a new instance of this Orion module.
        /// 
        /// !WARNING!
        /// DO NOT INTERACT WITH OTHER MODULES INSIDE THIS MODULE CONSTRUCTOR,
        /// INTERACT WITH OTHER MODULES IN THE INITIALIZE METHOD BELOW. 
        /// </summary>
        /// <param name="core">
        /// A reference to the Orion core who loaded this module.
        /// </param>
		protected OrionModuleBase(Orion core)
        {
            OrionModuleAttribute attrib = (OrionModuleAttribute)Attribute.GetCustomAttribute(GetType(), typeof(OrionModuleAttribute));

            if (attrib == null)
            {
                throw new Exception("Module does not display OrionModuleAttribute");
            }

            moduleAttr = attrib;
        }

        /// <summary>
        /// Called after all modules have been loaded, to initalize the module.
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// Called when Orion will run the module.
        /// </summary>
		public abstract void Run();

        #region IDisposable support
        ~OrionModuleBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed == true)
            {
                return;
            }

            if (disposing)
            {
                //Dispose IDisposables
            }

            //Set large fields to null
            //Release any unmanaged memory

            disposed = true;
        }

        #endregion

        public override string ToString()
        {
            return $"[OrionModule {ModuleName} v{ModuleVersion.ToString()}: Author={Author} Order={moduleAttr.Order}]";
        }
    }
}