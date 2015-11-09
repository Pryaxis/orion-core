using System.Collections.Generic;
using OTA;

namespace Orion.Commands.Commands
{
    public interface IOrionCommand
    {

        BasePlayer Sender { get; set; }
        CommandResult Run();
    }
}