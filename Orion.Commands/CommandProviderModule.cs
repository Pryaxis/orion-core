using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orion.Commands.Commands;
using Orion.Framework;
using OTA;

namespace Orion.Commands
{
    [OrionModule("Command Provider", "Nyx Studios", Description = "Allows for other modules to register commands for use in-game by players.")]
    public class CommandProviderModule : OrionModuleBase
    {
        //TODO: Hook into a chat hook and process command strings.
        //TODO: Config options such as what the command specifier character is.

        public readonly CommandManager Commands;

        public CommandProviderModule(Orion core) : base(core)
        {
            Commands = new CommandManager();
        }

        public override void Run()
        {
            
        }

        public void RunCommand(BasePlayer player, string commandString)
        {
            Commands.ParseAndCallCommand(player, commandString);
        }
    }
}
