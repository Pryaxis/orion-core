using Orion.Framework.Events;
using OTA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.Events
{
    public class ConsoleLineEventArgs : OrionEventArgs
    {
        public BasePlayer Player { get; set; }

        public string Line { get; set; }
    }
}
