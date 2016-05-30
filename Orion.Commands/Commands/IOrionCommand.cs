using OTAPI.Core;

namespace Orion.Commands.Commands
{
    public interface IOrionCommand
    {

        IEntity Sender { get; set; }
        CommandResult Run();
    }
}