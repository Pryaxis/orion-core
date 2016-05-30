using OTAPI.Core;

namespace Orion.Framework.Events
{
    public class ConsoleLineEventArgs : OrionEventArgs
    {
        public IEntity Player { get; set; }

        public string Line { get; set; }
    }
}
