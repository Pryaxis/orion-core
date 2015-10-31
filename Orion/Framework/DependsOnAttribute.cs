using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework
{
    /// <summary>
    /// Indicates that this module has a strict dependency on the availability of specified modules.
    /// Guarantees that by the time Initialize() gets called on the module, the module it depends on
    /// shall be initialized.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DependsOnAttribute : Attribute
    {
        protected Type[] moduleDepends;

        /// <summary>
        /// Gets the list of Module dependencies of which this module is marked to be dependent on.
        /// </summary>
        public Type[] ModuleDependencies => moduleDepends;

        public DependsOnAttribute(params Type[] moduleDepends)
        {
            this.moduleDepends = moduleDepends;
        }
    }
}
