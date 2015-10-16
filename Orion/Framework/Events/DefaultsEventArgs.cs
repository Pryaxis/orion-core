using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.Events
{
    public class DefaultsEventArgs<TObject, TInfo> : OrionEventArgs
    {
        public TObject Object { get; set; }

        public TInfo Info { get; set; }
    }
}
