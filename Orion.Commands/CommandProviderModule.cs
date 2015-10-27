using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Orion.Commands.Commands;
using Orion.Modules;
using Orion.Framework;
using Orion.Framework.Events;
using OTA;

namespace Orion.Commands
{
    //TODO: Allow commands to specify permissions.
    //TODO: Logging of exceptions and issues.
    //TODO: Hook into a chat hook and process command strings.
    //TODO: Config options such as what the command specifier character is.
    [OrionModule("Command Provider", "Nyx Studios", Description = "Allows for other modules to register commands for use in-game by players.")]
    [OrionDepends(typeof(Modules.Hooks.OTAPIHookModule), typeof(Modules.Console.ConsoleModule))]
    public class CommandProviderModule : OrionModuleBase
    {
        public readonly CommandManager Commands;
        private ConsolePlayer CPlayer { get; set; } = new ConsolePlayer();
       
        public CommandProviderModule(Orion core) : base(core)
        {
            Commands = new CommandManager();
            Core.Console.ConsoleLine += Console_ConsoleLine;
        }

        private void Console_ConsoleLine(object sender, ConsoleLineEventArgs e)
        {
            RunCommand(e.Player, e.Line);
        }

        public override void Initialize()
        {
            
        }

        public void RunCommand(BasePlayer player, string commandString)
        {
            Commands.ParseAndCallCommand(player, commandString);
        }
    }
}
