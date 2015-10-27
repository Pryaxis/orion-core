using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework
{
    /// <summary>
    /// Indicates that this module has a strict dependency on the availability of other modules.
    /// Guarantees that by the time Initialize() gets called on the module, the module it depends on
    /// shall be initialized.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class OrionDependsAttribute : Attribute
    {
        protected Type[] moduleDepends;

        public OrionDependsAttribute(params Type[] moduleDepends)
        {
            this.moduleDepends = moduleDepends;
        }

    }
}
